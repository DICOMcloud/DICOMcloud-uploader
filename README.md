
# DICOMcloud-uploader
Upload DICOM files to DICOMweb endpoint using .NET Core console 

## Command Line Args

If arguments not provided in the command line, you will get the chance to specify them at run time.
|arg| Description  |
|--|--|
| --dir | Optional - Path to DICOM directory  |
| --url | URL to a DICOMweb Store (aka STOW-RS) endpoint  |
| --pattern | Optional - File Pattern for DICOM directory to upload  |
| --batch | Optional - Integer for DICOMweb Store send batch  |

 ## Examples:
`DICOMcloudUploader.exe --dir "d:\dicomimages" --url "http://dicomcloud.azurewebsites.net/api/studies"` 
