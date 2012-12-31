REM Setting variables...
REM Name of the folder where the 'publish' from msbuild target will be performed
set publishFolder=..\Setup\publish\
REM Remove complete publish folder in order to be sure that evrything will be newly compiled
rmdir /S /Q %publishFolder%
REM Rebuild entire solution
msbuild /p:Configuration=Release /t:ReBuild ..\CowboyFS.Web.UI.sln
REM Publish your web site
msbuild /t:ResolveReferences;_CopyWebApplication /p:Configuration=Release;OutDir=%publishFolder%\bin\;WebProjectOutputDir=%publishFolder% ..\CowboyFS.Web.UI\CowboyFS.Web.UI.csproj
REM Delete debug and setup web.configs to prevent 'heat' from harveting it
del %publishFolder%Web.Debug.config
del %publishFolder%Web.Release.config
REM Harvest all content of published result
heat dir %publishFolder% -dr MYWEBWEBSITE -ke -srd -cg MyWebWebComponents -var var.publishDir -gg -out WebSiteContent.wxs
REM At last create an installer
candle -ext WixIISExtension -ext WixUtilExtension -ext WiXNetFxExtension -dpublishDir=%publishFolder% -dMyWebResourceDir=. IisConfiguration.wxs Product.wxs WebSiteContent.wxs Shortcuts.wxs CowboyFSUI.wxs
light -ext WixUIExtension -ext WixIISExtension -ext WixUtilExtension -ext WiXNetFxExtension -out bin\Release\CowboyFS.msi Product.wixobj WebSiteContent.wixobj Shortcuts.wixobj CowboyFSUI.wixobj IisConfiguration.wixobj
