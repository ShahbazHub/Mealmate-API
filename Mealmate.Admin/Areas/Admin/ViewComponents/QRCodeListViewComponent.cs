using Mealmate.Admin.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mealmate.Admin.Areas.Admin.ViewComponents
{
    public class QRCodeListViewComponent : ViewComponent
    {
        public QRCodeListViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync(List<int> QRCodeTypes, List<int> Tables)
        {
            var items = await GetItemsAsync(QRCodeTypes, Tables);
            return View(items);
        }

        private Task<List<QRCodeListViewModel>> GetItemsAsync(List<int> QRCodeTypes, List<int> Tables)
        {
            var result = new List<QRCodeListViewModel>()
            {
                new QRCodeListViewModel()
                {
                    QRCodeId = 1,
                    Table = "T - 1",
                    QRCodeType = "Bill Request",
                    Hall = "Hall No. 1",
                    Branch = "Branch No. 1",
                    QRCodeStateId = 1,
                    QRCodeState = "Active",
                    Generated = DateTime.Now
                },
                new QRCodeListViewModel()
                {
                    QRCodeId = 2,
                    Table = "T - 1",
                    QRCodeType = "Restroom Request",
                    Hall = "Hall No. 1",
                    Branch = "Branch No. 1",
                    QRCodeStateId = 1,
                    QRCodeState = "Active",
                    Generated = DateTime.Now
                },
                new QRCodeListViewModel()
                {
                    QRCodeId = 3,
                    Table = "T - 1",
                    QRCodeType = "Order Request",
                    Hall = "Hall No. 1",
                    Branch = "Branch No. 1",
                    QRCodeStateId = 1,
                    QRCodeState = "Active",
                    Generated = DateTime.Now
                },
                new QRCodeListViewModel()
                {
                    QRCodeId = 4,
                    Table = "T - 2",
                    QRCodeType = "Bill Request",
                    Hall = "Hall No. 1",
                    Branch = "Branch No. 1",
                    QRCodeStateId = 1,
                    QRCodeState = "Active",
                    Generated = DateTime.Now
                },
                new QRCodeListViewModel()
                {
                    QRCodeId = 5,
                    Table = "T - 2",
                    QRCodeType = "Restroom Request",
                    Hall = "Hall No. 1",
                    Branch = "Branch No. 1",
                    QRCodeStateId = 1,
                    QRCodeState = "Active",
                    Generated = DateTime.Now
                },
                new QRCodeListViewModel()
                {
                    QRCodeId = 6,
                    Table = "T - 2",
                    QRCodeType = "Order Request",
                    Hall = "Hall No. 1",
                    Branch = "Branch No. 1",
                    QRCodeStateId = 1,
                    QRCodeState = "Active",
                    Generated = DateTime.Now
                },
                new QRCodeListViewModel()
                {
                    QRCodeId = 7,
                    Table = "T - 3",
                    QRCodeType = "Bill Request",
                    Hall = "Hall No. 1",
                    Branch = "Branch No. 1",
                    QRCodeStateId = 1,
                    QRCodeState = "Active",
                    Generated = DateTime.Now
                },
                new QRCodeListViewModel()
                {
                    QRCodeId = 8,
                    Table = "T - 3",
                    QRCodeType = "Restroom Request",
                    Hall = "Hall No. 1",
                    Branch = "Branch No. 1",
                    QRCodeStateId = 1,
                    QRCodeState = "Active",
                    Generated = DateTime.Now
                },
                new QRCodeListViewModel()
                {
                    QRCodeId = 9,
                    Table = "T - 3",
                    QRCodeType = "Order Request",
                    Hall = "Hall No. 1",
                    Branch = "Branch No. 1",
                    QRCodeStateId = 1,
                    QRCodeState = "Active",
                    Generated = DateTime.Now
                },
            };

            return Task.FromResult(result);
        }
    }
}
