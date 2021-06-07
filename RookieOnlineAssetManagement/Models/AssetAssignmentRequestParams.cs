using Microsoft.AspNetCore.Mvc.ModelBinding;
using RookieOnlineAssetManagement.Atributes;
using RookieOnlineAssetManagement.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Models
{
    public class AssetAssignmentRequestParams
    {
        [ColumnSort("AssetId")]
        public SortBy? SortAssetId { get; set; }

        [ColumnSort("AssetName")]
        public SortBy? SortAssetName { get; set; }

        [ColumnSort("CategoryName")]
        public SortBy? SortCategoryName { get; set; }

        [BindNever]
        public string LocationId { get; set; }

        // [Required(ErrorMessage = "Current asset id cannot null")]
        public string CurrentAssetId { get; set; }

        public string Query { get; set; }
    }
}
