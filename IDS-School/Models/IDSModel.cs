using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IDS_School.Models
{
    public enum reaction { Like, Dislike ,Love, Care, HaHa, Wow, Sad, Angry }
    public enum Gender { Male, Female, Other }
    public enum FileType { doc, docx, img, Document}
    public static class Global
    {
        private static string rootFolderName => "_Files";

        public static string PATH_TOPIC => Path.Combine(rootFolderName, "Ideas");
        public static string PATH_TEMP { get { return Path.Combine(rootFolderName, "Temp"); } }
    }

    public class CUser : IdentityUser
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Birthday { get; set; }
        [Required]
        public Gender Gender { get; set; }

        public string Address { get; set; }
        //..
        [Required]
        public string StaffId { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<Idea> Ideas { get; set; }
        public virtual ICollection<View> Views { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Reaction> Reactions { get; set; }
    }
    public class Department
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter department name!")]
        [DisplayName("Department Name")]
        public string Name { get; set; }
        public virtual ICollection<CUser> Users { get; set; }
    }

    public class Idea
    {
        private const string V = "{0:dd/MM/yyyy hh:mm tt}";
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }

        [DisplayFormat(DataFormatString = V, ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }

        [DisplayFormat(DataFormatString = V, ApplyFormatInEditMode = true)]
        public DateTime LastModifiedDate { get; set; }

        public Boolean isAnoymous { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]        
        public string UserId { get; set; }
        public virtual CUser User { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [Required]
        public int SubmissionId { get; set; }
        public virtual Submission Submission { get; set; }

        public virtual ICollection<View> Views { get; set; }
        public virtual ICollection<File> Files { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Reaction> Reactions { get; set; }
    }

    public class File
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public FileType Type { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LastModifiedDate { get; set; }

        public int IdeaId { get; set; }
        public virtual Idea Idea { get; set; }
    }
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter Category name!")]
        [DisplayName("Category Name")]
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class Submission
    {
        private const string V = "{0:dd/MM/yyyy hh:mm tt}";
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter Name!")]
        [DisplayName("Submission Name")]
        public string Name { get; set; }
        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ClosureDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FinalClosureDate { get; set; }
    }
    public class View
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LastVisitedDate { get; set; }

        public string UserId { get; set; }
        public virtual CUser User { get; set; }

        public int IdeaId { get; set; }
        public virtual Idea Idea { get; set; }
    }
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public Boolean isAnoymous { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LastModifiedDate { get; set; }

        public string UserId { get; set; }
        public virtual CUser User { get; set; }

        public int IdeaId { get; set; }
        public virtual Idea Idea { get; set; }

        public ICollection<Reply> Replies { get; set; }
    }
    public class Reaction
    {
        public int Id { get; set; }
        public reaction reaction { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }

        public string UserId { get; set; }
        public virtual CUser User { get; set; }

        public int IdeaId { get; set; }
        public virtual Idea Idea { get; set; }
    }

    public class Reply
    {
        public int Id { get;  set; }
        public string Content { get; set; }
        public Boolean isAnoymous { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LastModifiedDate { get; set; }

        public string UserId { get; set; }
        public virtual CUser User { get; set; }

        public int CommentId { get; set; }
        public virtual Comment Comment { get; set; }

    }
}
