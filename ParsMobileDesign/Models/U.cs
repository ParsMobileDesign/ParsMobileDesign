using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Security.Claims;

namespace ParsMobileDesign
{
    public static class U
    {

        public const string defaultProductImage = "default.jpg";

        // User Roles
        public const string ManagerUser = "Manager";
        public const string ControllerUser = "Controller";
        public const string CustomerUser = "Customer";


        public const string ShoppingCartSession = "shoppingCart";
        public const string SubCategoryIdSession = "subCatId";

        public const byte OrderStatus_NotSubmitted = 0;
        public const byte OrderStatus_Submitted = 1;
        public const byte OrderStatus_InProcess = 2;
        public const byte OrderStatus_Ready = 3;
        public const byte OrderStatus_Completed = 4;
        public const byte OrderStatus_Canceled = 5;


        public const byte PaymentStatus_Pending = 0;
        public const byte PaymentStatus_Approved = 1;
        public const byte PaymentStatus_Rejected = 2;
        /// <summary>
        /// Save Files based on Input and then return saved File's name
        /// </summary>
        /// <param name="iWebHostEnvironment"></param>
        /// <param name="iWWWRootDesireFolder">
        /// folder name in WWWRoot folder
        /// </param>
        /// <param name="iFile">
        /// pass file control to this parameter
        /// </param>
        /// <param name="fileNameTobeSaved">
        /// desire file name
        /// </param>
        /// <returns></returns>
        /// 
        public static string getUserId(Controller iController)
        {
            var claims = (ClaimsIdentity)iController.User.Identity;
            var claim = claims.FindFirst(ClaimTypes.NameIdentifier);
            return claim != null ? claim.Value : "";
        }
        public static string SaveFileThenGetFileName(IWebHostEnvironment iWebHostEnvironment, string iWWWRootDesireFolder, IFormFileCollection iFile, string fileNameTobeSaved)
        {
            string extention = ".jpg";
            var uploadPath = Path.Combine(iWebHostEnvironment.WebRootPath, "images", iWWWRootDesireFolder);
            if (iFile.Count > 0)
            {
                extention = Path.GetExtension(iFile[0].FileName);
                var filenameComplete = fileNameTobeSaved + extention;
                using (var fileStream = new FileStream(Path.Combine(uploadPath, filenameComplete), FileMode.Create))
                {
                    iFile[0].CopyTo(fileStream);
                }
            }
            else
            {
                var defaultDocument = Path.Combine(uploadPath + "\\" + U.defaultProductImage);

                System.IO.File.Copy(defaultDocument, Path.Combine(uploadPath, fileNameTobeSaved + ".jpg"));

            }
            return @"\images\" + iWWWRootDesireFolder + "\\" + fileNameTobeSaved+ extention;


        }
    }

    public class StripeSettings
    {
        public string SecretKey { get; set; }
        public string PublishableKey { get; set; }
    }
}
