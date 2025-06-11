# ใช้ .NET 9 SDK เพื่อ build โค้ด
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# คัดลอกไฟล์และ restore dependencies
COPY ./WebApiProject.csproj ./
RUN dotnet restore

# คัดลอกไฟล์ทั้งหมดและ build
COPY . .
RUN dotnet publish -c Release -o out

# ใช้ .NET 9 Runtime สำหรับรันแอป
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# ระบุพอร์ตที่ต้องการ expose
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "WebApiProject.dll"]
