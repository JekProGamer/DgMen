# DgMen Health Reward

A CounterStrikeSharp plugin for CS2.

## Rules
- Headshot kill: add 100 HP.
- Body kill: add 25 HP.
- Health is capped at 100 HP.
- World kills, invalid attackers and suicides do not grant health.

The plugin also sets the killer's max health to 100 when a valid reward is applied.

## Install
1. Build with the .NET 10 SDK:
   `dotnet restore`
   `dotnet build -c Release`
2. Copy `bin/Release/net10.0/DgMenHealthReward.dll` to:
   `game/csgo/addons/counterstrikesharp/plugins/DgMenHealthReward/`
3. Restart the server or reload the plugin.

## Notes
This repository contains source code. The DLL must be built before installation. Test on your server's installed CounterStrikeSharp version before using it in production.
