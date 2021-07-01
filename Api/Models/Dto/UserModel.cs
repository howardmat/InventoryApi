﻿using System.ComponentModel.DataAnnotations;

namespace Api.Models.Dto
{
    public class UserModel
    {
        public int Id { get; set; }
        public string LocalId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? TenantId { get; set; }
    }
}
