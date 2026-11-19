using Npgsql;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();


app.MapGet("/teste-conexao", async () =>
{
    try
    {
        var connectionString = builder.Configuration
            .GetConnectionString("DefaultConnection");

        await using var conexao =
            new NpgsqlConnection(connectionString);

        await conexao.OpenAsync();

        return Results.Ok(new
        {
            mensagem = "Conexão com o banco realizada com sucesso!",
            banco = "biblioteca",
            status = "Conectado"
        });
    }
    catch (Exception ex)
    {
        return Results.Problem(
            "Não foi possível conectar ao banco: " + ex.Message
        );
    }
});

app.Run();