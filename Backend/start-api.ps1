# Script para iniciar la API de Productos Descontinuados
# Ejecutar con: .\start-api.ps1

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  API de Productos Descontinuados" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Verificar si MySQL está corriendo
Write-Host "Verificando MySQL..." -ForegroundColor Yellow
$mysqlProcess = Get-Process -Name "mysqld" -ErrorAction SilentlyContinue

if ($null -eq $mysqlProcess) {
    Write-Host "??  MySQL no está corriendo. Por favor inicia MySQL Server." -ForegroundColor Red
    Write-Host ""
    Read-Host "Presiona Enter para continuar de todos modos o Ctrl+C para cancelar"
} else {
    Write-Host "? MySQL está corriendo" -ForegroundColor Green
}

Write-Host ""

# Verificar connection string
Write-Host "Verificando configuración..." -ForegroundColor Yellow
$appSettings = Get-Content "appsettings.json" | ConvertFrom-Json

if ($appSettings.ConnectionStrings.DefaultConnection -like "*yourpassword*") {
    Write-Host "??  ADVERTENCIA: La contraseña de MySQL está por defecto." -ForegroundColor Red
    Write-Host "   Edita appsettings.json antes de continuar." -ForegroundColor Red
    Write-Host ""
    $continue = Read-Host "¿Deseas continuar de todos modos? (s/n)"
    if ($continue -ne "s" -and $continue -ne "S") {
        exit
    }
} else {
    Write-Host "? Connection string configurado" -ForegroundColor Green
}

Write-Host ""
Write-Host "Iniciando API..." -ForegroundColor Yellow
Write-Host ""

# Ejecutar la API
dotnet run

# Si llegas aquí, la API se detuvo
Write-Host ""
Write-Host "API detenida." -ForegroundColor Yellow
Read-Host "Presiona Enter para salir"
