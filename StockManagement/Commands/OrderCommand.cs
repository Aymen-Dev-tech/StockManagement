using Aspose.Pdf;
using StockManagement.DALs;
using StockManagement.Models;
using StockManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO.Packaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Xps.Packaging;
using System.Windows.Controls;

namespace StockManagement.Commands
{
    public class OrderCommand : CommandBase
    {
        private readonly OrdersViewModel _orderViewModel;
        private readonly ProductMapper _productMapper;
        private static readonly string dataDir = $"C:\\Users\\{Environment.UserName}\\Documents";
        public OrderCommand(OrdersViewModel orderViewModel, ProductMapper productMapper)
        {
            _orderViewModel = orderViewModel;
            _productMapper = productMapper;
        }

        public override void Execute(object parameter)
        {
            var selectedProducts = _orderViewModel.FilteredProductsCategories
                                                       .Where(p => p.IsIncludedInOrder && p.OrderQuantity > 0)
                                                       .ToList();
            if (selectedProducts.Count == 0) return;
            // Update each product
            _orderViewModel.IsLoading = true;
            Task.Run(() =>
            {
                foreach (var product in selectedProducts)
                {
                    product.Quantity -= product.OrderQuantity;
                    product.LastUsageTime = DateTime.Now;

                    var updatedProduct = new ProductsCategories(
                        product.ProductCode, product.ProductName, product.ProductImage,
                        product.MaxQuantity, product.MinQuantity, product.Quantity,
                        null, product.LastUsageTime, product.OrderQuantity, product.IsIncludedInOrder,
                        new CategoryModel(product.CategoryCode, product.CategoryName));

                    try
                    {
                        _productMapper.Update(updatedProduct);
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Application.Current.Dispatcher.Invoke(() =>
                        {
                            MessageBox.Show($"Error updating product {product.ProductCode}: {ex.Message}");
                        });
                    }
                }

                try
                {
                    var fetchedProducts = _productMapper.FindAllProductsCategories();
                    // Switch back to the UI thread to update the collection
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        _orderViewModel.FilteredProductsCategories.Clear();
                        foreach (var product in fetchedProducts)
                        {
                            _orderViewModel.FilteredProductsCategories.Add(new OrdersProductsCategories(product));
                        }

                        Document document = SaveOrder(selectedProducts);
                        MessageBoxResult result = MessageBox.Show("Order Saved Successfully.\n" +
                            "Do you Want To Print it?", "Order Creation", MessageBoxButton.YesNo, MessageBoxImage.Information);
                        if (result == MessageBoxResult.Yes)
                        {
                            PrintOrder(document);
                        }
                    });
                }
                catch (Exception ex)
                {
                    // Handle any errors during the refresh
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        MessageBox.Show($"Error refreshing product list: {ex.Message}");
                    });
                }
                finally
                {
                    // Set IsLoading to false on the UI thread
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        _orderViewModel.IsLoading = false;
                    });
                }
            });
        }

        public Document SaveOrder(List<OrdersProductsCategories> ordersProducts)
        {
            DataTable dt = new DataTable("Order");
            dt.Columns.Add("Product Code", typeof(Int32));
            dt.Columns.Add("Product Name", typeof(string));
            dt.Columns.Add("Order Quantity", typeof(Int32));
            dt.Columns.Add("Category Name", typeof(string));
            DataRow dr;
            for (int i = 0; i < ordersProducts.Count; i++)
            {
                dr = dt.NewRow();
                dr[0] = ordersProducts[i].ProductCode;
                dr[1] = ordersProducts[i].ProductName;
                dr[2] = ordersProducts[i].OrderQuantity;
                dr[3] = ordersProducts[i].CategoryName;
                dt.Rows.Add(dr);
            }
            Document document = new Document();
            document.Pages.Add();
            // Initializes a new instance of the Table
            Aspose.Pdf.Table table = new Aspose.Pdf.Table();
            // Set column widths of the table
            table.ColumnWidths = "100 100 100 100";
            // Set the table border color as LightGray
            table.Border = new Aspose.Pdf.BorderInfo(Aspose.Pdf.BorderSide.All, .5f, Aspose.Pdf.Color.FromRgb(System.Drawing.Color.LightGray));
            // Set the border for table cells
            table.DefaultCellBorder = new Aspose.Pdf.BorderInfo(Aspose.Pdf.BorderSide.All, .5f, Aspose.Pdf.Color.FromRgb(System.Drawing.Color.LightGray));
            table.ImportDataTable(dt, true, 0, 0, ordersProducts.Count, 4);

            // Add table object to first page of input document
            document.Pages[1].Paragraphs.Add(table);
            var outputFileName = System.IO.Path.Combine(dataDir, $"Order-{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.pdf");
            // Save updated document containing table object
            document.Save(outputFileName);
            return document;
        }

        private void PrintOrder(Document document)
        {

            var memoryStream = new MemoryStream();
            document.Save(memoryStream, SaveFormat.Xps);

            var package = Package.Open(memoryStream);

            var inMemoryPackageName = $"memorystream://{Guid.NewGuid()}.xps";
            var packageUri = new Uri(inMemoryPackageName);

            //Add package to PackageStore
            PackageStore.AddPackage(packageUri, package);

            var xpsDoc = new XpsDocument(package, CompressionOption.Maximum, inMemoryPackageName);
            var documentViewer = new DocumentViewer
            {
                Document = xpsDoc.GetFixedDocumentSequence()
            };

            var window = new Window
            {
                Content = documentViewer,
                Title = "Print Preview",
                Width = 800,
                Height = 600
            };

            window.Show();
            xpsDoc.Close();
        }

    }
}
