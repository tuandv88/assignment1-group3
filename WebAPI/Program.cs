
using BusinessObject;
using WebAPI.Service;
using Microsoft.OpenApi.Models;
using WebAPI.Dto;
using WebAPI.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebAPI {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(option => {
                option.SwaggerDoc("v1", new OpenApiInfo() {
                    Version = "v1",
                    Title = "Assginment 1",
                    Description = "This is a project of group 3"
                });
                var securityScheme = new OpenApiSecurityScheme {
                    Name = "JWT Authentication",
                    Description = "Enter your JWT token: ",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                };

                option.AddSecurityDefinition("Bearer", securityScheme);

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                };

                option.AddSecurityRequirement(securityRequirement);
            });
            builder.Services
            .AddAuthentication(x => {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x => {
                x.RequireHttpsMetadata = true;// Phải có https
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JWTService.SECRET_KEY)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            builder.Services.AddAuthorization(options => {
                options.AddPolicy("Admin", policy => policy.RequireRole("admin"));
            });

            builder.Services.AddDbContext<TDbContext>();

            builder.Services.AddScoped<ProductService>();
            builder.Services.AddScoped<CategoryService>();
            builder.Services.AddScoped<OrderService>();
            builder.Services.AddScoped<OrderDetailService>();
            builder.Services.AddScoped<StaffService>();

            builder.Services.AddSingleton<JWTService>();

            builder.Services.AddHttpContextAccessor();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
            //app.MapGet("/signin", () => "User Authenticated Successfully!").RequireAuthorization();
            app.MapPost("/authenticate", (StaffDto user, JWTService authService)
    => authService.GenerateToken(user));
            app.Run();
        }
    }
}
