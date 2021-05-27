using Microsoft.AspNetCore.Mvc.ModelBinding;
using RookieOnlineAssetManagement.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Models
{
    public class AssetRequestParams
    {
        public StateAsset[] State { get; set; }
        public string[] CategoryId { get; set; }
        public string Query { get; set; }
        public SortBy? SortCode { get; set; }
        public SortBy? SortName { get; set; }
        public SortBy? SortCate { get; set; }
        public SortBy? SortState { get; set; }
        [BindNever]
        public string LocationId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
