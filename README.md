
# DICOMcloud-uploader
Upload DICOM files to DICOMweb endpoint using .NET Core console 

## Command Line Args

If arguments not provided in the command line, you will get the chance to specify them at run time.
|arg| Description  |
|--|--|
| --dir | Optional - Path to DICOM directory  |
| --url | URL to a DICOMweb Store (aka STOW-RS) endpoint  |
| --pattern | File Pattern for DICOM directory to upload. Default is "\*.\*"  |
| --batch | Optional - Integer for DICOMweb Store send batch. Default is 5  |

 ## Examples:
```DICOMcloudUploader.exe --dir "d:\dicomimages" --url "http://dicomcloud.azurewebsites.net/api/studies"```

```DICOMcloudUploader.exe --dir "d:\dicomimages" --url "http://dicomcloud.azurewebsites.net/api/studies" --pattern "*.dcm" --batch 12```