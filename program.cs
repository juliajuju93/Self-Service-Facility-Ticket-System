using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//adding DBContext SQL Azure Database
builder.Services.AddDbContext<FacilityRequetsDb>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SQLDatabaseConnectionString")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//GET all facility requests
app.MapGet("/allrequests", async (FacilityRequetsDb db) =>
    await db.FacilityRequests.ToListAsync());


//GET facility ID request
app.MapGet("/allrequests/{id}", async (FacilityRequetsDb db, int id) =>
    await db.FacilityRequests.FindAsync(id)
        is FacilityRequest request
        ? Results.Ok(request)
        : Results.NotFound("Sorry, facility ID not found"));

//POST new facility request
app.MapPost("/allrequests", async (FacilityRequetsDb db, FacilityRequest request) =>
{
    db.FacilityRequests.Add(request);
    await db.SaveChangesAsync();

    return Results.Created($"/allrequests/{request.Id}", request);
});

//PUT facility request
app.MapPut("/allrequests/{id}", async (int id, FacilityRequest inputrequest, FacilityRequetsDb db) =>
{
    var itemToUpdate = await db.FacilityRequests.FindAsync(id);

    if (itemToUpdate is null) return Results.NotFound("Sorry, facility ID not found");

    if (inputrequest.IdStatus != null)
        itemToUpdate.IdStatus = inputrequest.IdStatus;

    if (inputrequest.IdType != null)
        itemToUpdate.IdType = inputrequest.IdType;

    if (inputrequest.IdRequestor != null)
        itemToUpdate.IdRequestor = inputrequest.IdRequestor;

    if (inputrequest.IdRequestorEmail != null)
        itemToUpdate.IdRequestorEmail = inputrequest.IdRequestorEmail;

    if (inputrequest.IdRequestorDepartment != null)
        itemToUpdate.IdRequestorDepartment = inputrequest.IdRequestorDepartment;

    if (inputrequest.IdRequestorPhone != null)
        itemToUpdate.IdRequestorPhone = inputrequest.IdRequestorPhone;

    if (inputrequest.IdAssignment != null)
        itemToUpdate.IdAssignment = inputrequest.IdAssignment;

    await db.SaveChangesAsync();

    return Results.Ok(itemToUpdate);
});

//DELETE facility ID request
app.MapDelete("/allrequests/{id}", async (int id, FacilityRequetsDb db) =>
{
    if (await db.FacilityRequests.FindAsync(id) is FacilityRequest request)
    {
        db.FacilityRequests.Remove(request);
        await db.SaveChangesAsync();
        return Results.Ok(request);
    }

    return Results.NotFound();
});


app.Run();

class FacilityRequest
{
    [Column("id")]
    public int Id { get; set; }
    [Column("id_status")]
    public string? IdStatus { get; set; }
    [Column("id_type")]
    public string? IdType { get; set; }
    [Column("id_requestor")]
    public string? IdRequestor { get; set; }
    [Column("id_requestor_email")]
    public string? IdRequestorEmail { get; set; }
    [Column("id_requestor_department")]
    public string? IdRequestorDepartment { get; set; }
    [Column("id_requestor_phone")]
    public string? IdRequestorPhone { get; set; }
    [Column("id_assignment")]
    public string? IdAssignment { get; set; }

}

class FacilityRequetsDb : DbContext
{
    public FacilityRequetsDb(DbContextOptions<FacilityRequetsDb> options)
        : base(options) { }

    public DbSet<FacilityRequest> FacilityRequests => Set<FacilityRequest>();
}
