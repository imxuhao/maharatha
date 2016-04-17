using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Abp.Auditing;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.IO;
using Abp.Runtime.Session;
using Abp.UI;
using CAPS.CORPACCOUNTING.Authorization.Users.Profile.Dto;
using CAPS.CORPACCOUNTING.Storage;
using System;

namespace CAPS.CORPACCOUNTING.Authorization.Users.Profile
{
    [AbpAuthorize]
    public class ProfileAppService : CORPACCOUNTINGAppServiceBase, IProfileAppService
    {
        private readonly AppFolders _appFolders;
        private readonly IBinaryObjectManager _binaryObjectManager;

        public ProfileAppService(
            AppFolders appFolders,
            IBinaryObjectManager binaryObjectManager)
        {
            _appFolders = appFolders;
            _binaryObjectManager = binaryObjectManager;
        }

        public async Task<CurrentUserProfileEditDto> GetCurrentUserProfileForEdit()
        {
            var user = await GetCurrentUserAsync();
            return user.MapTo<CurrentUserProfileEditDto>();
        }

        public async Task UpdateCurrentUserProfile(CurrentUserProfileEditDto input)
        {
            var user = await GetCurrentUserAsync();
            input.MapTo(user);
            CheckErrors(await UserManager.UpdateAsync(user));
        }

        [DisableAuditing]
        public async Task ChangePassword(ChangePasswordInput input)
        {
            var user = await GetCurrentUserAsync();
            CheckErrors(await UserManager.ChangePasswordAsync(user.Id, input.CurrentPassword, input.NewPassword));
        }

        public async Task<byte[]> UpdateProfilePicture(UpdateProfilePictureInput input)
        {
            string tempProfilePicturePath;
            if (ReferenceEquals(_appFolders.TempFileDownloadFolder, null))
                tempProfilePicturePath = input.TempFilePath;
            else
                tempProfilePicturePath = Path.Combine(_appFolders.TempFileDownloadFolder, input.FileName);

            byte[] byteArray;

            using (var fsTempProfilePicture = new FileStream(tempProfilePicturePath, FileMode.Open))
            {
                using (var bmpImage = new Bitmap(fsTempProfilePicture))
                {
                    var width = input.Width == 0 ? bmpImage.Width : input.Width;
                    var height = input.Height == 0 ? bmpImage.Height : input.Height;
                    var bmCrop = bmpImage.Clone(new Rectangle(input.X, input.Y, width, height), bmpImage.PixelFormat);

                    using (var stream = new MemoryStream())
                    {
                        bmCrop.Save(stream, bmpImage.RawFormat);
                        stream.Close();
                        byteArray = stream.ToArray();
                    }
                }
            }

            if (byteArray.LongLength > 102400) //100 KB
            {
                throw new UserFriendlyException(L("ResizedProfilePicture_Warn_SizeLimit"));
            }

            var user = await UserManager.GetUserByIdAsync(AbpSession.GetUserId());

            if (user.ProfilePictureId.HasValue)
            {
                await _binaryObjectManager.DeleteAsync(user.ProfilePictureId.Value);
            }

            var storedFile = new BinaryObject(byteArray);
            await _binaryObjectManager.SaveAsync(storedFile);

            user.ProfilePictureId = storedFile.Id;

            FileHelper.DeleteIfExists(tempProfilePicturePath);
            return byteArray;
        }
    }
}