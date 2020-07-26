Write-Host "Initializing local aws environment using localstack" -ForegroundColor Green
docker-compose -f docker-compose-localstack.yml up -d

Write-Host "Localstack started!" -ForegroundColor Green

Write-Host "Deploying politico" -ForegroundColor Yellow
cd politico
serverless deploy --env=dev --force
cd ..
Write-Host "Politico deployed succesfuly!" -ForegroundColor Green

Write-Host "Deploying kickstart" -ForegroundColor Yellow
cd kickstart
serverless deploy --env=dev --force
cd ..
Write-Host "Kickstart deployed succesfuly!" -ForegroundColor Green

Write-Host "Deploying DynamoDB" -ForegroundColor Yellow
cd gaia/infrastructure/dynamo
serverless deploy --env=dev --force
cd ../../..
Write-Host "DynamoDB deployed succesfuly!" -ForegroundColor Green

Write-Host "Deploying GaiaAPI" -ForegroundColor Yellow
cd gaia/services/api/src/Gaia.API
dotnet lambda package --verbosity quiet --configuration Debug --framework netcoreapp3.1 --output-package ../../bin/Gaia.API.zip
cd ../../
serverless deploy --env=dev --force
cd ../../..
Write-Host "GaiaAPI deployed succesfuly!" -ForegroundColor Green

Write-Host "Deploying Processor Lambda" -ForegroundColor Yellow
cd gaia/services/processor
serverless deploy --env=dev --force
cd ../../..
Write-Host "Processor Lambda deployed succesfuly!" -ForegroundColor Green