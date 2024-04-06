# Azuri Cosmos Devkit Updater (AzuriCDU)

## Description
AzuriCDU is a command-line tool for updating the Cosmos Devkit. It automates the process of fetching the latest versions of Cosmos components from their respective GitHub repositories.

## Options
- `--update`: Updates the Cosmos Devkit.
- `--version`: Displays the version of AzuriCDU.

## Commands
- `--update`: Initiates the update process. This command clones the latest versions of Cosmos, IL2CPU, XSharp, and Common repositories from GitHub.
- `--version`: Displays the current version of AzuriCDU.

## Common Errors and Solutions
- **Error**: Unable to find MiniGit.
  - **Solution**: Ensure that MiniGit is located in the same directory as AzuriCDU. Download MiniGit and place it in the appropriate directory if it's missing. (Can be found in this repo)

- **Error**: Failed to delete old CosmosInstall folder.
  - **Solution**: Ensure that the CosmosInstall folder is not in use by any other process. Close any applications that might be using it and try again. Or run as administrator.

- **Error**: Cloning Cosmos repository failed.
  - **Solution**: Check your internet connection. If the issue persists, try again later.

- **Error**: Unable to set registry values.
  - **Solution**: Ensure that the program has the necessary permissions to access and modify the registry. Run the program as an administrator if needed.

## License
This software is provided under the MIT License. See the [LICENSE](LICENSE) file for more details.

## Author
AzuriCDU was created by AzureianGH.

