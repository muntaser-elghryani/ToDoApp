namespace ToDoApp.Dtos.UserDtos;

    public class CreateUserDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public int? TeamId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }


