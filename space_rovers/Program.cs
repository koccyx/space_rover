using System.Reflection;
using System.Text;
using Amazon.Runtime;
using Amazon.S3;
using Application.Interfaces;
using Application.Services;
using Application.Services.impl;
using Application.utils.jwt;
using AutoMapper;
using Infrastracture;
using Infrastracture.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace space_rovers;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		builder.Services.AddControllers();
		builder.Services.AddAuthorization();
		builder.Services.AddAuthentication(x =>
		{
			x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
		}).AddJwtBearer(x =>
			x.TokenValidationParameters = new TokenValidationParameters()
			{
				ValidateIssuer = false,
				ValidateAudience = false,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtOptions:SecretKey"] ?? throw new InvalidOperationException())) 
			}
		);

		builder.Services.AddOpenApi();

		builder.Services.Configure<S3Settings>(builder.Configuration.GetSection("S3Settings"));
		builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));
		builder.Services.AddSingleton<IAmazonS3>(sp =>
		{
			var s3Settings = sp.GetRequiredService<IOptions<S3Settings>>().Value;
			var config = new AmazonS3Config()
			{
				ServiceURL = s3Settings.ServiceUrl,
				AuthenticationRegion = s3Settings.RegionName
			};

			var credentials = new BasicAWSCredentials(s3Settings.AccessKey, s3Settings.SecretKey);


			return new AmazonS3Client(credentials, config);
		});

		builder.Services.AddAutoMapper(typeof(MappingProfile.MappingProfile));
		builder.Services.AddScoped<IJwtProvider, JwtProvider>();
		builder.Services.AddScoped<IPasswordHasher, PasswordHasher> ();
		
		builder.Services.AddScoped<ICompanyService, CompanyService>();
		builder.Services.AddScoped<IUserService, UserService>();
		builder.Services.AddScoped<IFolderService, FolderService>();
		builder.Services.AddDbContext<ApplicationDbContext>();
		
		var app = builder.Build();

		if (app.Environment.IsDevelopment())
		{
			app.MapOpenApi();
			app.UseSwaggerUI(options => { options.SwaggerEndpoint("/openapi/v1.json", "Demo Api"); });
		}

		app.UseHttpsRedirection();

		app.UseAuthorization();
		app.MapControllers();
		app.Run();
	}
}