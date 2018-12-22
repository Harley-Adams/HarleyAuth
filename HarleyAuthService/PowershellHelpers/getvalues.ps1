$url = "https://localhost:44323/api/values/Auth"

$authToken = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJoYXJsZXkuZy5hZGFtc0BnbWFpbC5jb20iLCJqdGkiOiIxOTQ4OGI2NS02ZTc2LTQ2ZDItOTdmZC04NWE4Y2UxZjEzNWUiLCJpYXQiOjE1NDUxMjI1NTQsInJvbCI6ImFwaV9hY2Nlc3MiLCJpZCI6ImE4ZDQyMTlhLTVjNmYtNDhlMC04YjUwLTc4NWMwYzM3NzJkNSIsIm5iZiI6MTU0NTEyMjU1MywiZXhwIjoxNTQ1MTI5NzUzLCJpc3MiOiJ3ZWJBcGkiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjUwMDAvIn0.ej6nTqgBYqiVIMffCBhH0m-Jkm57cEhETDYF3Hel_2A"

$headers = @{
    'Authorization' = $authToken 
}

Invoke-RestMethod -Uri $url -Method 'Get' -Headers $headers