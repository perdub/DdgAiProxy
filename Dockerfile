#building
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS builder
WORKDIR /app
COPY . .
RUN dotnet publish web/DdgAiProxy.csproj -c Release -r linux-musl-x64 --self-contained true -o /app/out

#runtime
FROM alpine:latest AS runtime
EXPOSE 5000
WORKDIR /app
COPY --from=builder /app/out .
RUN apk add --no-cache libstdc++
RUN chmod 777 /app/DdgAiProxy
ENTRYPOINT ["/app/DdgAiProxy"]
