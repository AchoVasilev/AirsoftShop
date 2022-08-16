namespace AirsoftShop.WebApi.Infrastructure;

using AirsoftShop.Data.Models;
using AirsoftShop.Data.Models.Images;
using Data.Persistence;
using Microsoft.EntityFrameworkCore;
using Models;
using Newtonsoft.Json;

internal static class ApplicationBuilderExtensions
{
    internal static IApplicationBuilder UseRoutingAndAuth(this IApplicationBuilder app)
        => app.UseRouting()
            .UseAuthentication()
            .UseAuthorization();

    internal static async Task<IApplicationBuilder> PrepareDatabase(this IApplicationBuilder app)
    {
        using var scopedServices = app.ApplicationServices.CreateScope();

        var serviceProvider = scopedServices.ServiceProvider;
        var data = serviceProvider.GetRequiredService<ApplicationDbContext>();

        await data.Database.MigrateAsync();

        await SeedCouriers(data);
        await SeedCategories(data);
        await SeedCities(data);
        
        return app;
    }

    private static async Task SeedCouriers(ApplicationDbContext data)
    {
        if (data.Couriers.Any())
        {
            return;
        }

        var couriers = new List<Courier>
        {
            new Courier()
            {
                Name = "Speedy",
                DeliveryDays = 2,
                DeliveryPrice = 23.00m,
                Image = new Image
                {
                    Url = "https://res.cloudinary.com/dpo3vbxnl/image/upload/v1649915945/BgAirsoft/speedy_q4gl0r.png",
                    Name = "Speedy",
                    Extension = "png"
                }
            },
            new Courier()
            {
                Name = "Еконт",
                DeliveryDays = 5,
                DeliveryPrice = 16.00m,
                Image = new Image
                {
                    Url = "https://res.cloudinary.com/dpo3vbxnl/image/upload/v1649915945/BgAirsoft/econt_bg_hdnull.png",
                    Name = "Econt",
                    Extension = "png"
                }
            },
            new Courier()
            {
                Name = "Европът",
                DeliveryDays = 10,
                DeliveryPrice = 13.00m,
                Image = new Image
                {
                    Url =
                        "https://res.cloudinary.com/dpo3vbxnl/image/upload/v1649915945/BgAirsoft/%D0%B5vropat_q4ut3h.png",
                    Name = "Evropat",
                    Extension = "png"
                }
            },
        };

        await data.Couriers.AddRangeAsync(couriers);
        await data.SaveChangesAsync();
    }

    private static async Task SeedCategories(ApplicationDbContext data)
    {
        if (data.Categories.Any())
        {
            return;
        }

        var categories = new List<Category>
        {
            new Category()
            {
                Name = "Еърсофт оръжия",
                Image = new CategoryImage()
                {
                    Url = "https://res.cloudinary.com/dpo3vbxnl/image/upload/v1649099134/BgAirsoft/gun_eog8eg.jpg",
                    Name = "gun_eog8eg",
                    Extension = "jpg",
                },
                SubCategories = new List<SubCategory>
                {
                    new SubCategory()
                    {
                        Name = "Електрически оръжия(AEG)",
                    },
                    new SubCategory()
                    {
                        Name = "Пистолети"
                    },
                    new SubCategory()
                    {
                        Name = "Помпи"
                    },
                    new SubCategory()
                    {
                        Name = "Газови"
                    },
                    new SubCategory()
                    {
                        Name = "Пневматични"
                    }
                }
            },
            new Category()
            {
                Name = "Тактическа екипировка",
                Image = new CategoryImage()
                {
                    Url = "https://res.cloudinary.com/dpo3vbxnl/image/upload/v1649099236/BgAirsoft/vest_gkm21l.jpg",
                    Name = "vest_gkm21l",
                    Extension = "jpg",
                },
                SubCategories = new List<SubCategory>
                {
                    new SubCategory()
                    {
                        Name = "Тактически жилетки",
                    },
                    new SubCategory()
                    {
                        Name = "Комуникационна екипировка"
                    },
                    new SubCategory()
                    {
                        Name = "Защита за глава"
                    },
                    new SubCategory()
                    {
                        Name = "Джобове за пълнители"
                    },
                    new SubCategory()
                    {
                        Name = "Чанти и куфари"
                    },
                    new SubCategory()
                    {
                        Name = "Кобури за пистолети",
                    },
                    new SubCategory()
                    {
                        Name = "Системи за хидратиране"
                    },
                    new SubCategory()
                    {
                        Name = "Ръкавици"
                    },
                    new SubCategory()
                    {
                        Name = "Маски"
                    },
                    new SubCategory()
                    {
                        Name = "Раници"
                    },
                    new SubCategory()
                    {
                        Name = "Ремъци"
                    },
                    new SubCategory()
                    {
                        Name = "Ножове и остриета"
                    },
                    new SubCategory()
                    {
                        Name = "Наколенки"
                    },
                    new SubCategory()
                    {
                        Name = "Колани"
                    }
                }
            },
            new Category()
            {
                Name = "Поддръжка",
                Image = new CategoryImage()
                {
                    Url = "https://res.cloudinary.com/dpo3vbxnl/image/upload/v1649099235/BgAirsoft/smith_fzks4b.jpg",
                    Name = "smith_fzks4b",
                    Extension = "jpg",
                },
                SubCategories = new List<SubCategory>
                {
                    new SubCategory()
                    {
                        Name = "Смазка",
                    },
                    new SubCategory()
                    {
                        Name = "Пружини"
                    },
                    new SubCategory()
                    {
                        Name = "Моторчета"
                    },
                    new SubCategory()
                    {
                        Name = "Цеви"
                    },
                    new SubCategory()
                    {
                        Name = "Спусъци"
                    }
                }
            },
            new Category()
            {
                Name = "Добавки",
                Image = new CategoryImage()
                {
                    Url = "https://res.cloudinary.com/dpo3vbxnl/image/upload/v1649099269/BgAirsoft/attach_b1skox.jpg",
                    Name = "attach_b1skox",
                    Extension = "jpg",
                },
                SubCategories = new List<SubCategory>
                {
                    new SubCategory()
                    {
                        Name = "Светлини",
                    },
                    new SubCategory()
                    {
                        Name = "Лазери"
                    },
                    new SubCategory()
                    {
                        Name = "Оптики и бързомери"
                    },
                    new SubCategory()
                    {
                        Name = "Ръкохватки"
                    },
                    new SubCategory()
                    {
                        Name = "Пълнители"
                    }
                }
            },
            new Category()
            {
                Name = "Аксесоари",
                Image = new CategoryImage()
                {
                    Url =
                        "https://res.cloudinary.com/dpo3vbxnl/image/upload/v1649099269/BgAirsoft/accessories_htmctb.jpg",
                    Name = "attach_b1skox",
                    Extension = "jpg",
                },
                SubCategories = new List<SubCategory>
                {
                    new SubCategory()
                    {
                        Name = "Топчета BBS",
                    },
                    new SubCategory()
                    {
                        Name = "Газ"
                    },
                    new SubCategory()
                    {
                        Name = "Батерии"
                    },
                    new SubCategory()
                    {
                        Name = "Зарядни устройства"
                    },
                    new SubCategory()
                    {
                        Name = "Гранати"
                    }
                }
            },
            new Category()
            {
                Name = "Облекло",
                Image = new CategoryImage()
                {
                    Url = "https://res.cloudinary.com/dpo3vbxnl/image/upload/v1649099269/BgAirsoft/apparel_aqzalg.png",
                    Name = "apparel_aqzalg",
                    Extension = "png",
                },
                SubCategories = new List<SubCategory>
                {
                    new SubCategory()
                    {
                        Name = "Униформи",
                    },
                    new SubCategory()
                    {
                        Name = "Обувки"
                    },
                    new SubCategory()
                    {
                        Name = "Дрехи"
                    },
                    new SubCategory()
                    {
                        Name = "За глава"
                    }
                }
            },
        };

        await data.Categories.AddRangeAsync(categories);
        await data.SaveChangesAsync();
    }

    private static async Task SeedCities(ApplicationDbContext data)
    {
        if (data.Cities.Any())
        {
            return;
        }

        var citiesJson = await File.ReadAllTextAsync("Datasets/Cities.json");
        var citiesDto = JsonConvert.DeserializeObject<CityDto[]>(citiesJson);
        var cities = new List<City>();

        foreach (var cityDto in citiesDto)
        {
            var city = new City()
            {
                Name = cityDto.Name,
                ZipCode = cityDto.ZipCode
            };

            cities.Add(city);
        }

        await data.Cities.AddRangeAsync(cities);
        await data.SaveChangesAsync();
    }
}