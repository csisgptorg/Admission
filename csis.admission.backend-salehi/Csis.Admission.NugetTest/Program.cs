var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCsisAdmission(x => {
    x.BaseUrl = "https://localhost:5000";
    //x.BaseUrl = "http://dev.csis.ir:4022/api";
    x.ApiKey = "ee3a7970-c2b4-48b4-8e01-80969d465925-23d528b4-e356-4939-a064-91c3a2a6cf3f-84865611-7620-4ca2-b0f9-04";
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
