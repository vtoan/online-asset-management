using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json.Converters;
using RookieOnlineAssetManagement.Atributes;
using RookieOnlineAssetManagement.Enums;

namespace RookieOnlineAssetManagement.Models
{
    public class UserRequestParmas
    {
        [ColumnSort("StaffCode")]
        public SortBy? SortCode { get; set; }
        [ColumnSort("FullName")]
        public SortBy? SortFullName { get; set; }
        [ColumnSort("JoinedDate")]
        public SortBy? SortDate { get; set; }
        [ColumnSort("RoleName")]
        public SortBy? SortType { get; set; }
        [BindNever]
        public string LocationId { get; set; }
        public TypeUser[] Type { get; set; }
        public string Query { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}