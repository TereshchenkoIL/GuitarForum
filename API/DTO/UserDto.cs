﻿namespace API.DTO
{
    public class UserDto
    {
        public string DisplayName { get; set; }
        public string Token { get; set; }
        public string Image { get; set; }
        public string Bio { get; set; }
        public bool isAdmin { get; set; }
        public string Username { get; set; }
    }
}