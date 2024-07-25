using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 60000000; // 60 MB
});

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlite("Data Source=sqlite.db"));



// CORS politikasını tanımla
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://127.0.0.1:5500")
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials();
        });
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

// Uygulamaya CORS Middleware ekle
app.UseCors("AllowSpecificOrigin");

// kullanıcı kayıt
app.MapPost("/register", async (DataContext context, User user) =>
{
    var existingUser = await context.Users.FirstOrDefaultAsync(x => x.Username == user.Username);
    if (existingUser != null)
    {
        return Results.BadRequest("Username already exists");
    }
    
    context.Users.Add(user);
    await context.SaveChangesAsync();
    return Results.Created($"/users/{user.Id}", user);
});

// kullanıcı listeleme
app.MapGet("/users", async (DataContext context) =>
{
    var users = await context.Users.ToListAsync();
    return Results.Ok(users);
});

// kullanıcı bilgilerini username ile alma 
app.MapGet("/users/{username}", async (DataContext context, string username) =>
{
    var user = await context.Users.FirstOrDefaultAsync(x => x.Username == username);
    if (user == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(user);
});

// kullanıcı silme
app.MapDelete("/users/{username}", async (DataContext context, string username) =>
{
    var user = await context.Users.FirstOrDefaultAsync(u => u.Username == username);
    
    if (user == null)
    {
        return Results.NotFound();
    }

    context.Users.Remove(user);
    await context.SaveChangesAsync();
    return Results.Ok($"User {username} deleted");
});

// kullanıcı giriş
app.MapPost("/login", async (DataContext context, User user) =>
{
    var user1 = await context.Users.FirstOrDefaultAsync(x => x.Username == user.Username && x.Password == user.Password);
    if (user1 == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(user1);
});

// tüm soruları listeleme
app.MapGet("/questions", async (DataContext context) =>
{
    var questions = await context.Questions.ToListAsync();
    return Results.Ok(questions);
});

// id ile soru getirme
app.MapGet("/questions/{id}", async (DataContext context, int id) =>
{
    var question = await context.Questions.FindAsync(id);
    if (question == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(question);
});

// soruyu ders ve adedi üzerinden getirme
app.MapGet("/questions/category/{soru_dersi}/{soru_adedi}", async (DataContext context, string soru_dersi, int soru_adedi) =>
{
    var questions = await context.Questions
        .Where(x => x.soru_dersi == soru_dersi)
        .OrderBy(x => EF.Functions.Random()) // rastgele sıralama için
        .Take(soru_adedi)
        .ToListAsync();
        
    if (questions == null || questions.Count == 0)
    {
        return Results.NotFound();
    }

    return Results.Ok(questions);
});


// soruyu ders üzerinden getirme
app.MapGet("/questions/category/{soru_dersi}", async (DataContext context, string ders) =>
{
    var questions = await context.Questions.Where(x => x.soru_dersi == ders).ToListAsync();
    if (questions == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(questions);
});



// soru silme
app.MapDelete("/questions/{id}", async (DataContext context, int id) =>
{
    var question = await context.Questions.FindAsync(id);
    if (question == null)
    {
        return Results.NotFound();
    }

    context.Questions.Remove(question);
    await context.SaveChangesAsync();
    return Results.Ok($"Question {id} deleted");
});

// veri tabanındaan kullanıcının score bilgisini güncelleme
app.MapPut("/users/{username}/score", async (DataContext context, string username, User user) =>
{
    var user1 = await context.Users.FirstOrDefaultAsync(u => u.Username == username);

    if (user1 == null)
    {
        return Results.NotFound();
    }

    user1.Score += user.Score;
    user1.Total_questions += user.Total_questions;
    user1.Correct_answers += user.Correct_answers;
    user1.Wrong_answers += user.Wrong_answers;

    await context.SaveChangesAsync();
    return Results.Ok(user1);
});

// soru ekleme
app.MapPost("/questions", async (DataContext context, Question question) =>
{
    context.Questions.Add(question);
    await context.SaveChangesAsync();
    return Results.Created("/questions",question);
});

// Dosya yükleme fonksiyonu
app.MapPost("/upload_file", async context =>
{
    if (!context.Request.HasFormContentType)
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync("Invalid form content");
        return;
    }

    var form = await context.Request.ReadFormAsync();
    var file = form.Files.GetFile("dosya_yukle");

    if (file == null || file.Length == 0)
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync("No file selected");
        return;
    }

    var path = Path.Combine(Directory.GetCurrentDirectory(), "web/uploads", file.FileName);
    Console.WriteLine(path);


    if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "web/uploads")))
    {
        Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "web/uploads"));
    }

    using (var stream = new FileStream(path, FileMode.Create))
    {
        await file.CopyToAsync(stream);
    }

    context.Response.StatusCode = 200;
    await context.Response.WriteAsync($"File uploaded successfully: {file.FileName}");
});



app.Run();
