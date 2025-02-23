# Blogging API
This is a simple Blogging API to save and retrieve both blog posts and comments.

## How to run
1. Set your connection string on `appsettings.json`.
2. Restore NuGet packages for the whole solution.
3. On Package Manager Console, set target project to `BloggingAPI.Data`.
4. Also on Package Manager Console, run `Update-database` in order to create the database.
5. Set the startup profile to your preference.
6. Run the project.

## Next steps
- Prepare the application for Docker
- Add more functionalities (i.e., update blog post, delete blog post, etc)
- Add unit testing