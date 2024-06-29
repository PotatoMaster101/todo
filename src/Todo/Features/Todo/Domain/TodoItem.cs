using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Todo.Features.Todo.Domain;

/// <summary>
/// Represents a TODO item.
/// </summary>
[Table("todo_item")]
public class TodoItem
{
    /// <summary>
    /// Gets or sets the creation date.
    /// </summary>
    [Column("creation_date")]
    public DateTime CreationDate { get; set; } = DateTime.Now.ToUniversalTime();

    /// <summary>
    /// Gets or sets the creation user ID.
    /// </summary>
    [Column("creation_user_id")]
    [Required]
    [StringLength(int.MaxValue)]
    public required string CreationUserId { get; set; }

    /// <summary>
    /// Gets or sets whether this item has been done.
    /// </summary>
    [Column("done")]
    public bool Done { get; set; }

    /// <summary>
    /// Gets or sets the item ID.
    /// </summary>
    [Column("id")]
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the todo message.
    /// </summary>
    [Column("message")]
    [Required]
    [StringLength(int.MaxValue)]
    public required string Message { get; set; }

    /// <summary>
    /// Gets or sets the todo title.
    /// </summary>
    [Column("title")]
    [Required]
    [StringLength(Constants.MaxTitleLength)]
    public required string Title { get; set; }

    /// <summary>
    /// Gets or sets the update date.
    /// </summary>
    [Column("update_date")]
    public DateTime UpdateDate { get; set; } = DateTime.Now.ToUniversalTime();
}
