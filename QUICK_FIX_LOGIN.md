# Quick Fix for Login Issue

## Immediate Solution (Choose One)

### Option 1: Use Diagnostic Endpoint (Easiest) ⭐

1. **Make sure your backend API is running** on `https://localhost:7022`

2. **Open Swagger UI**: Go to `https://localhost:7022/swagger`

3. **Find the endpoint**: `POST /api/Diagnostic/reset-admin-password`

4. **Click "Try it out"**

5. **Leave the body empty** (or use `{"newPassword": "Admin@123"}`)

6. **Click "Execute"**

7. **You should see**: `"success": true` and a message confirming the admin user was created/reset

8. **Now try logging in** with:
   - Email: `admin@drcoffee.com`
   - Password: `Admin@123`

### Option 2: Restart Backend API

1. **Stop your backend API** (Ctrl+C in the terminal)

2. **Start it again** - The seed method will automatically:
   - Check if admin user exists
   - Reset password to `Admin@123` if user exists
   - Create user if it doesn't exist

3. **Check the console output** for messages like:
   - `✅ Created admin user: admin@drcoffee.com` OR
   - `✅ Reset password for existing admin user: admin@drcoffee.com`

4. **Try logging in** with:
   - Email: `admin@drcoffee.com`
   - Password: `Admin@123`

### Option 3: Use PowerShell/curl

Open PowerShell and run:

```powershell
# Reset admin password (creates user if doesn't exist)
Invoke-WebRequest -Uri "https://localhost:7022/api/diagnostic/reset-admin-password" `
  -Method POST `
  -ContentType "application/json" `
  -Body '{}' `
  -SkipCertificateCheck

# Check if admin exists
Invoke-WebRequest -Uri "https://localhost:7022/api/diagnostic/check-admin" `
  -Method GET `
  -SkipCertificateCheck
```

## Verify It Works

After using any option above, test the login:

1. Go to `https://localhost:7022/swagger`
2. Find `POST /api/auth/login`
3. Use this body:
   ```json
   {
     "email": "admin@drcoffee.com",
     "password": "Admin@123"
   }
   ```
4. Click "Execute"
5. You should get a 200 response with a JWT token

## Still Not Working?

1. **Check backend console** for error messages
2. **Verify database connection** - Make sure SQL Server is running
3. **Check database exists** - The connection string uses `Database=DrCoffeeDB`
4. **Try the diagnostic endpoint** to see if user exists:
   - `GET https://localhost:7022/api/diagnostic/check-admin`

## What Was Fixed

✅ Enhanced seed method to reset password on startup  
✅ Added diagnostic endpoints to check/reset admin user  
✅ Improved error logging in AuthController  
✅ Auto-create admin user if it doesn't exist via diagnostic endpoint

