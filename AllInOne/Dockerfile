FROM microsoft/aspnetcore:2.0
ENV clientId="2AEDC767-9BB1-47FF-8FD0-311F850559F3"
ENV clientSecret="20754391-B13E-45E3-B2EB-060BF14C3137"
ENV apiServer="https://api.sitename.com"
ENV contentServer="https://api.sitename.com"
ENV productName="AllInOne"
ENV defaultImage="///img/default/bitmap.png"

# Google Analytics Code
ENV googleAnalyticsCode=UA-102313314-3



#FROM redis
#COPY redis.conf /usr/local/etc/redis/redis.conf
#CMD [ "redis-server", "/usr/local/etc/redis/redis.conf" ]


ARG source
WORKDIR /app
EXPOSE 80
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["dotnet", "AllInOne.dll"]

