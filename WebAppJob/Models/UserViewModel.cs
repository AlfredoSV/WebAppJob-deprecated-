﻿using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;
namespace WebAppJob.Models
{
	public class UserViewModel
	{
		public Guid Id { get; private set; }
		public string UserName { get; private set; }
		public string Password { get; private set; }
		public string ConfirmPasword { get; private set; }
		public DateTime DateCreated { get; private set; }
		public Guid RoleId { get; set; }

		[Required]
		[StringLength(15)]
		public string Name { get;  set; }
		public string LastName { get;  set; }
		public int Age { get;  set; }
		public string Address { get; set; }
		public string Email { get; set; }
		public IFormFile CvFile { get; set; }

        public UserViewModel()
        {
            
        }
        private UserViewModel(Guid id, string userName, DateTime dateCreated,
			Guid roleId, string name, string lastName, int age, string address, string email)
		{
			Id = id;
			UserName = userName;
			DateCreated = dateCreated;
			RoleId = roleId;
			Name = name;
			LastName = lastName;
			Age = age;
			Address = address;
			Email = email;
		}

		public static UserViewModel Create(Guid id, string userName, 
			DateTime dateCreated, Guid roleId, string name, 
			string lastName, int age, string address, string email)
		{
			return new UserViewModel( id,  userName,  dateCreated,  roleId,
				name,  lastName,  age,  address,  email);		
		}
	}
}
