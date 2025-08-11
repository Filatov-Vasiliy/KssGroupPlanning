# Makefile для C# (.NET 8) проекта

PROJECT = KssGroupPlanning

all: build

restore:
	dotnet restore

build: restore
	dotnet build $(PROJECT).csproj --configuration Release

run: build
	dotnet run --project $(PROJECT).csproj

test:
	dotnet test

publish:
	dotnet publish $(PROJECT).csproj --configuration Release --output ./publish

clean:
	dotnet clean
	rm -rf ./publish

.PHONY: all restore build run test publish clean
