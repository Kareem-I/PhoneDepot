using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhoneDepot.Models;
using PhoneDepot.Data.Enum;
using Microsoft.AspNetCore.Identity;
using PhoneDepot.Data.Static;

namespace PhoneDepot.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                //Phonemanufacturer  
                if (!context.PhoneManufacturer.Any())
                {
                    context.PhoneManufacturer.AddRange(new List<PhoneManufacturer>()
                    {
                        new PhoneManufacturer()
                        {
                            ManufacturerName = "Samsung",
                            ImageURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/24/Samsung_Logo.svg/2560px-Samsung_Logo.svg.png",
                        },

                         new PhoneManufacturer()
                        {
                            ManufacturerName = "Apple",
                            ImageURL = "https://logos-download.com/wp-content/uploads/2017/07/Apple_Logo_1998.png"
                        },

                          new PhoneManufacturer()
                        {
                            ManufacturerName = "Huawei",
                            ImageURL = "https://1000logos.net/wp-content/uploads/2018/10/Huawei-logo.jpg",
                        }
                    });

                    context.SaveChanges();

                }


                //Phone
                if (!context.Phone.Any())
                {
                    context.Phone.AddRange(new List<Phone>()
                    {
                        new Phone()
                        {
                            PhoneName = "Samsung S22",
                            ImageURL = "https://www.elgiganten.se/image/dv_web_D1800010021323129/414961/samsung-galaxy-s22-5g-smartphone-8128gb-phantom-black--pdp_zoom-3000--pdp_main-960.jpg",
                            PhoneDescription = "Samsung Android Phone",
                            Price = 11000,
                            OSCategory = OSCategory.Android,
                            ManufacturerId=3
                        },
                        new Phone()
                        {
                            PhoneName = "Iphone 14",
                            ImageURL = "https://assets.mmsrg.com/isr/166325/c1/-/ASSET_MMS_97288548/fee_786_587_png/APPLE-iPhone-14-Pro-5G-128GB---6.1%22-Smartphone---Djuplila",
                            PhoneDescription = "Apple IOS Phone",
                            Price = 14000,
                            OSCategory = OSCategory.IOS,
                            ManufacturerId=2
                        },

                         new Phone()
                        {
                            PhoneName = "Huawei p50",
                            ImageURL = "https://www.proshop.se/Images/915x900/3067260_e977460850b9.jpg",
                            PhoneDescription = "Huaweu HarmonyOS Phone",
                            Price = 7000,
                            OSCategory = OSCategory.HarmonyOS,
                            ManufacturerId=1
                        }

                     });

                    context.SaveChanges();

                }


            }
        }


        public static async Task SeedASPIdentity(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));






                //Users

                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "admin@phonedepot.co";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Admin User",
                        UserName = "admin-user",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };

                    await userManager.CreateAsync(newAdminUser, "1234?Ab");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);

                }





                string appUserEmail = "user@phonedepot.co";


                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = "Application User",
                        UserName = "app-user",
                        Email = appUserEmail,
                       EmailConfirmed = true
                    };

                    //appUser = await userManager.FindByEmailAsync(appUserEmail);
                    //await userManager.AddToRoleAsync(appUser, UserRoles.User);


                    await userManager.CreateAsync(newAppUser, "1234?Ab");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);



                }

                ///Stopp här
                ///
                //var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                //if (adminUser == null)
                //{
                //    var newAdminUser = new AppUser()
                //    {
                //        FullName = "Admin User",
                //        UserName = "admin-user",
                //        Email = adminUserEmail,
                //        EmailConfirmed = true
                //    };
                //    await userManager.CreateAsync(newAdminUser, "adminphonedepot");
                //    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);


                //}



                //var appUser = await userManager.FindByEmailAsync(appUserEmail);
                //if (appUser == null)
                //{
                //    var newAppUser = new AppUser()
                //    {
                //        FullName = "Application User",
                //        UserName = "app-user",
                //        Email = appUserEmail,
                //        EmailConfirmed = true
                //    };
                //    await userManager.CreateAsync(newAppUser, "userphonedepot");
                //    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                //}
            }
        }
    }}
