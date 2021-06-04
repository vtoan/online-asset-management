using Microsoft.AspNetCore.Mvc.ModelBinding;
using RookieOnlineAssetManagement.Atributes;
using RookieOnlineAssetManagement.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Models
{
    public class AssetRequestParams
    {
        [ColumnSort("AssetId")]
        public SortBy? SortCode { get; set; }
        [ColumnSort("AssetName")]
        public SortBy? SortName { get; set; }
        [ColumnSort("CategoryName")]
        public SortBy? SortCate { get; set; }
        [ColumnSort("State")]
        public SortBy? SortState { get; set; }
        public StateAsset[] State { get; set; }
        public string Query { get; set; }
        public string[] CategoryId { get; set; }
        [BindNever]
        public string LocationId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
