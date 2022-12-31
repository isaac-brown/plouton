$response = Invoke-WebRequest http://localhost:8080/realms/plouton/protocol/openid-connect/token `
    -Method Post `
    -Headers @{ 'Content-Type' = 'application/x-www-form-urlencoded' } `
    -Body 'grant_type=password&client_id=test-client&username=isaac-brown&password=password&client_secret=Bj1ueFgn7VmjDbQXOZoJOwCw5Xbqqmpq'

$accessToken = ($response.Content | ConvertFrom-Json).access_token
Write-Host 'Bearer' $accessToken