using CleanArchitectureExample.Domain.Entities;
using CleanArchitectureExample.Domain.Entities.Identity;
using CleanArchitectureExample.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Infrastructure.Persistence.SeedData
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Đảm bảo database đã được migrate
                context.Database.Migrate();

                // Kiểm tra và thêm dữ liệu cho bảng Market
                if (!context.Markets.Any())
                {
                    context.Markets.AddRange(
                        new Market { MarketId = 1 , MarketName = "Nhật Bản" },
                        new Market { MarketId = 2, MarketName = "Hàn Quốc" },
                        new Market { MarketId = 3, MarketName = "Đài Loan" },
                        new Market { MarketId = 4, MarketName = "Lào" },
                        new Market { MarketId = 5, MarketName = "Campuchia" }
                    );
                    context.SaveChanges();
                }

                if (!context.Products.Any())
                {
                    var product = new Product
                    {
                        ProductName = "Xịt giảm đau nhức xương khớp tức thì",
                        Description = "ĐAU ĐẦU GỐI NÊN DÙNG XỊT NÓNG HAY XỊT LẠNH?\r\n⚡ Đau đầu gối có thể do nhiều nguyên nhân như thoái hóa khớp, chấn thương, viêm khớp… Vậy khi nào nên dùng xịt nóng? Khi nào nên dùng xịt lạnh?\r\n🔥 Khi đau lâu ngày, không sưng không đỏ, hãy dùng XỊT NÓNG Mollifynovo Warm&Pain Relief Spray:\r\n✔️ Giúp giãn mạch máu, tăng cường lưu thông máu, giảm co cứng cơ.\r\n✔️ Phù hợp với đau do thoái hóa khớp, đau mạn tính, tê mỏi kéo dài.\r\n✔️ Không nên dùng khi khớp đang sưng viêm cấp tính.\r\n❄ Khi đầu gối sưng viêm to, đỏ rát hoặc vừa mới bị chấn thương trong vòng 48 giờ, hãy dùng XỊT LẠNH Mollifynovo Cold Spray:\r\n✔️ Giúp giảm viêm, giảm sưng, làm dịu cơn đau nhanh chóng.\r\n✔️ Thích hợp cho đau do chấn thương, bong gân, va đập, viêm khớp cấp.\r\n✔️ Không nên dùng khi đau do thoái hóa hoặc đau mạn tính lâu ngày.",
                        CreateAt = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"), // Format chuẩn
                        Price = 120000,
                        Markets = new List<Market>() // Khởi tạo danh sách Market
                    };
                    List<int> marketIds = new List<int> { 1, 2, 3, 4 }; // Danh sách các ID cần lấy
                    List<Market> listMarket = context.Markets
                            .Where(m => marketIds.Contains(m.MarketId))
                            .ToList();

                    product.Markets.AddRange(listMarket);

                    context.SaveChanges();
                }

                // Kiểm tra và thêm dữ liệu cho bảng Department
                if (!context.Departments.Any())
                {
                    context.Departments.AddRange(
                        new Department { DepartmentId = 1, DepartmentName = "Ban Giám Đốc" },
                        new Department { DepartmentId = 2, DepartmentName = "Phòng Kế Toán" },
                        new Department { DepartmentId = 3, DepartmentName = "Phòng Marketing" },
                        new Department { DepartmentId = 4, DepartmentName = "Phòng Sale" },
                        new Department { DepartmentId = 5, DepartmentName = "Đối tác vận chuyển" }
                    );
                    context.SaveChanges();
                }

                // Kiểm tra và thêm dữ liệu cho bảng ShippingPartner
                if (!context.ShippingPartners.Any())
                {
                    context.ShippingPartners.AddRange(
                        new ShippingPartner { ShippingPartnerId = 1, PartnerName = "DHL" , MarketId = 1 ,Region = "JP"},
                        new ShippingPartner { ShippingPartnerId = 2, PartnerName = "Viettel Post", MarketId = 2, Region = "KR" },
                        new ShippingPartner { ShippingPartnerId = 3, PartnerName = "J&T Express", MarketId = 3, Region = "TW" }
                    );
                    context.SaveChanges();
                }

                UserProfile userProfile = context.UserProfiles.FirstOrDefault();
                // Kiểm tra và thêm dữ liệu cho bảng Page
                if (!context.Pages.Any() && userProfile !=null)
                {
                    context.Pages.AddRange(
                        new Page { PageId = 1, PageName = "YBA Gia Định", PageLink = "https://www.facebook.com/ChihoiYBA.GiaDinh",ProductId = 1, CreateBy = userProfile.UserId}   
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
