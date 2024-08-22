﻿using DirectoryPermissionManagement.Configs;
using DirectoryPermissionManagement.Models;
using DirectoryPermissionManagement.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DirectoryPermissionManagement.Services
{
    public class FolderService
    {
        private readonly FolderRepository _folderRepository;
        private readonly PermissionRepository _permissionRepository;

        public FolderService(FolderRepository folderRepository, PermissionRepository permissionRepository)
        {
            _folderRepository = folderRepository;
            _permissionRepository = permissionRepository;
        }

        public Folder? GetByIdOfUser(int id, int userId)
        {
            var result = _folderRepository.GetById(id);
            return result;
        }

        public Folder? Insert(Folder folder)
        {
            if (_folderRepository.HadNameAndDriveIdAndParrentFolderId(folder.Name, folder.DriveId, (int?)folder.ParrentFolderId))
            {
                return null;
            }
            var result = _folderRepository.Insert(folder);

            return result;
        }

        public Folder? Update(int id, Folder folder)
        {
            if (!_folderRepository.IsExisted(id))
            {
                return null;
            }
            folder.Id = id;
            var result = _folderRepository.Update(folder);
            return result;
        }

        public bool Delete(int id)
        {
            var result = _folderRepository.GetById(id);
            if (result == null)
            {
                return false;
            }

            _folderRepository.Delete(result);
            return true;
        }
    }
}
