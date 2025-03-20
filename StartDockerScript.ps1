$CurrentDirectory = Get-Location
CD "$CurrentDirectory"

# Ensure UTF-8 encoding
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8

Write-Host "Descargant última versió de les imatges" -ForegroundColor Green
docker-compose pull

Write-Host "Iniciant Docker Compose" -ForegroundColor Green
docker-compose up -d 

Write-Host "Esperant que MySQL estigui llest..." -ForegroundColor Yellow
Start-Sleep -Seconds 10  # Espera inicial, ajusta segons sigui necessari

# Comprova si MySQL està llest
$maxRetries = 10
$retry = 0
while ($retry -lt $maxRetries) {
    $result = docker exec mysql-container mysqladmin ping -hlocalhost -uroot -p"123456Aa" 2>$null
    if ($result -match "mysqld is alive") {
        Write-Host "MySQL està llest!" -ForegroundColor Green
        break
    }
    Write-Host "Esperant MySQL... Intent $($retry+1)/$maxRetries" -ForegroundColor Yellow
    Start-Sleep -Seconds 3
    $retry++
}

if ($retry -ge $maxRetries) {
    Write-Host "MySQL no s'ha iniciat correctament." -ForegroundColor Red
    exit(1)
}

Write-Host "Omplint la base de dades..." -ForegroundColor Yellow
Get-Content "$CurrentDirectory/init.sql" -Encoding UTF8 | docker exec -i mysql-container mysql -uroot -p"123456Aa" sql7761283 --default-character-set=utf8


Write-Host "Les dades s'han inserit correctament." -ForegroundColor Green

pause  # Per evitar que es tanqui automàticament
