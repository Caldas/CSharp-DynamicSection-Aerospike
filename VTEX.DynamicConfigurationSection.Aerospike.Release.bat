ECHO OFF
del nuget\*.nupkg /Q
cls
ECHO CRIANDO PACOTE NUGET...
ECHO ----------------------------
nuget pack ".\Public Libraries\VTEX.Configuration.DynamicSection.Aerospike\VTEX.Configuration.DynamicSection.Aerospike.csproj" -Build -Properties Configuration=Release -OutputDirectory nuget
ECHO ----------------------------
pause