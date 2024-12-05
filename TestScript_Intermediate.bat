@echo off
set DEBUG_MODE=true
dotnet run

REM ---------------------------------------
echo 1) Benutzer registrieren
curl -i -X POST http://localhost:12000/users --header "Content-Type: application/json" -d "{\"username\":\"kienboec\", \"password\":\"daniel\"}"
curl -i -X POST http://localhost:12000/users --header "Content-Type: application/json" -d "{\"username\":\"admin\", \"password\":\"istrator\"}"
REM ---------------------------------------

echo 2) Benutzer einloggen
curl -i -X POST http://localhost:12000/sessions --header "Content-Type: application/json" -d "{\"username\":\"kienboec\", \"password\":\"daniel\"}"
curl -i -X POST http://localhost:12000/sessions --header "Content-Type: application/json" -d "{\"username\":\"admin\", \"password\":\"istrator\"}"
REM ---------------------------------------

echo 3) Benutzerinformationen abrufen
curl -i -X GET http://localhost:12000/users/kienboec --header "Authorization: Bearer kienboec-testToken"
curl -i -X GET http://localhost:12000/users/admin --header "Authorization: Bearer admin-testToken"
REM ---------------------------------------

echo 4) Pakete erstellen (nur Admin)
curl -i -X POST http://localhost:12000/packages --header "Content-Type: application/json" --header "Authorization: Bearer admin-testToken" -d "[{\"Id\":\"1\", \"Name\":\"WaterGoblin\", \"Damage\":10.0, \"Element\":\"water\", \"Type\":\"monster\"}, {\"Id\":\"2\", \"Name\":\"FireSpell\", \"Damage\":15.0, \"Element\":\"fire\", \"Type\":\"spell\"}, {\"Id\":\"3\", \"Name\":\"NormalGoblin\", \"Damage\":8.0, \"Element\":\"normal\", \"Type\":\"monster\"}, {\"Id\":\"4\", \"Name\":\"WaterSpell\", \"Damage\":12.0, \"Element\":\"water\", \"Type\":\"spell\"}, {\"Id\":\"5\", \"Name\":\"FireDragon\", \"Damage\":20.0, \"Element\":\"fire\", \"Type\":\"monster\"}]"
REM ---------------------------------------

echo 5) Pakete kaufen
curl -i -X POST http://localhost:12000/transactions/packages --header "Authorization: Bearer kienboec-testToken"
REM ---------------------------------------

echo 6) Karten anzeigen (nach Paketkauf)
curl -i -X GET http://localhost:12000/cards --header "Authorization: Bearer kienboec-testToken"
REM ---------------------------------------

echo 7) Fehlerhafte Paketerstellung (kein Admin)
curl -i -X POST http://localhost:12000/packages --header "Content-Type: application/json" --header "Authorization: Bearer kienboec-testToken" -d "[{\"Id\":\"6\", \"Name\":\"InvalidCard\", \"Damage\":5.0, \"Element\":\"normal\", \"Type\":\"monster\"}]"
REM ---------------------------------------

echo Test abgeschlossen!
pause
