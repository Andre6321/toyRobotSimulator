# Build and Run Script for PowerShell

Write-Host "=================================="
Write-Host "  Toy Robot Simulator - Build"
Write-Host "=================================="
Write-Host ""

# Check if .NET SDK is installed
Write-Host "Checking for .NET SDK..." -ForegroundColor Cyan
$dotnetVersion = dotnet --version 2>$null
if ($LASTEXITCODE -ne 0) {
    Write-Host "ERROR: .NET SDK is not installed!" -ForegroundColor Red
    Write-Host "Please install .NET 8.0 SDK or later from https://dotnet.microsoft.com/download"
    exit 1
}
Write-Host "Found .NET SDK version: $dotnetVersion" -ForegroundColor Green
Write-Host ""

# Restore dependencies
Write-Host "Restoring dependencies..." -ForegroundColor Cyan
dotnet restore
if ($LASTEXITCODE -ne 0) {
    Write-Host "ERROR: Failed to restore dependencies!" -ForegroundColor Red
    exit 1
}
Write-Host "Dependencies restored successfully." -ForegroundColor Green
Write-Host ""

# Build the solution
Write-Host "Building solution..." -ForegroundColor Cyan
dotnet build --configuration Release
if ($LASTEXITCODE -ne 0) {
    Write-Host "ERROR: Build failed!" -ForegroundColor Red
    exit 1
}
Write-Host "Build completed successfully." -ForegroundColor Green
Write-Host ""

# Run tests
Write-Host "Running tests..." -ForegroundColor Cyan
dotnet test --configuration Release --no-build
if ($LASTEXITCODE -ne 0) {
    Write-Host "WARNING: Some tests failed!" -ForegroundColor Yellow
} else {
    Write-Host "All tests passed!" -ForegroundColor Green
}
Write-Host ""

# Run the application
Write-Host "=================================="
Write-Host "  Starting Application"
Write-Host "=================================="
Write-Host ""
dotnet run --project ToyRobotSimulator.Console --configuration Release --no-build
