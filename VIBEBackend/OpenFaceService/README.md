# OpenFace API Endpoint

The OpenFace docker container has an endpoint to trigger an analyse of avatar images and video's. It can also show all
files currently saved in the blob storage.

Start the service using docker compose and go to http://localhost:7030/swagger to get a Swagger UI.

The GET request will show all files and the POST request will trigger an analysis.

After the analysis is done, you will find a new CSV file within same container and folder as the original image which
contains the results. This file has the same name as the image.

## Building the OpenFace Docker Image

The openface image is based on the image that was used in the original project. This one used `algebr/openface:latest`
as the base image and added a Python Django REST API to it.

For this project the Python API is replaced by a .NET API. To be able to run .NET applications in the algebr image, a
new base image was created on which dotnet aspruntime was installed.
This base image is publicly available on Docker Hub and is called `nickthijssen/openface-api`.

If the image becomes unavailable in the future, you can rebuild it by pulling the `algebr/openface:latest` image and
install dotnet on it again. To do this, an install script (
official script from Microsoft, see [link](https://docs.microsoft.com/en-us/dotnet/core/install/linux-scripted-manual))
was used. In the Dockerfile, this script needs to be copied first and then executed
to install dotnet. Because the installation of dotnet with this script is a bit finicky, it is better to build the image
separately from the others and enable extended output during the build process. To do this, run the following command:

```shell
docker compose build openface-api --no-cache --progress=plain
```

## Running Locally (Currently Not Possible)

The current OpenFaceService can only be run within a docker container. The reason for this, is that it uses the Linux
directory structure and needs the preinstalled OpenFace binaries provided by the `algebr/openface` image. Some work was
done to be able to detect if the application is running on Windows or Linux and depending on which one it is running it
can select the OpenFace executable and change the output directory for the results.

In theory, the application should be able to run on Windows by downloading the OpenFace
release [GitHub Link](https://github.com/TadasBaltrusaitis/OpenFace), changing the executable path to the executable and
changing the directory formats to the Windows format.

The logic for running on different operating systems can be found in the
class [OpenFaceServiceContext](./Data/Context/OpenFaceServiceContext.cs). To be able to run the application locally,
OpenFace needs to be present on the machine. Next, the application needs to be extended further to be able to provide
the path to the OpenFace executable and the results directory.
