[1mdiff --git a/.gitignore b/.gitignore[m
[1mindex e9633e1..5315dcc 100644[m
[1m--- a/.gitignore[m
[1m+++ b/.gitignore[m
[36m@@ -18,12 +18,6 @@[m [mobj/[m
 .idea/[m
 *.code-workspace[m
 [m
[31m-# Visual Studio[m
[31m-.vs/[m
[31m-*.vc.db[m
[31m-*.VC.db[m
[31m-*.pdb[m
[31m-[m
 # --- Logs and temp files ---[m
 *.log[m
 *.tmp[m
[36m@@ -36,6 +30,3 @@[m [mobj/[m
 appsettings.*.json[m
 .env[m
 *.secrets.json[m
[31m-[m
[31m-#基础尚未开发的子项目[m
[31m-apps/mobile/MerchantApp/[m
[1mdiff --git a/OnlineShop b/OnlineShop[m
[1mnew file mode 160000[m
[1mindex 0000000..0976aa0[m
[1m--- /dev/null[m
[1m+++ b/OnlineShop[m
[36m@@ -0,0 +1 @@[m
[32m+[m[32mSubproject commit 0976aa02c9c8a09c3a70e87cdefabb6add0b995b[m
[1mdiff --git a/apps/backend/API/.vs/API/DesignTimeBuild/.dtbcache.v2 b/apps/backend/API/.vs/API/DesignTimeBuild/.dtbcache.v2[m
[1mnew file mode 100644[m
[1mindex 0000000..9f2cbae[m
Binary files /dev/null and b/apps/backend/API/.vs/API/DesignTimeBuild/.dtbcache.v2 differ
[1mdiff --git a/apps/backend/API/.vs/API/config/applicationhost.config b/apps/backend/API/.vs/API/config/applicationhost.config[m
[1mnew file mode 100644[m
[1mindex 0000000..cdd2df8[m
[1m--- /dev/null[m
[1m+++ b/apps/backend/API/.vs/API/config/applicationhost.config[m
[36m@@ -0,0 +1,1026 @@[m
[32m+[m[32m<?xml version="1.0" encoding="UTF-8"?>[m
[32m+[m[32m<!--[m
[32m+[m
[32m+[m[32m    IIS configuration sections.[m
[32m+[m
[32m+[m[32m    For schema documentation, see[m
[32m+[m[32m    %IIS_BIN%\config\schema\IIS_schema.xml.[m
[32m+[m[41m    [m
[32m+[m[32m    Please make a backup of this file before making any changes to it.[m
[32m+[m
[32m+[m[32m    NOTE: The following environment variables are available to be used[m
[32m+[m[32m          within this file and are understood by the IIS Express.[m
[32m+[m
[32m+[m[32m          %IIS_USER_HOME% - The IIS Express home directory for the user[m
[32m+[m[32m          %IIS_SITES_HOME% - The default home directory for sites[m
[32m+[m[32m          %IIS_BIN% - The location of the IIS Express binaries[m
[32m+[m[32m          %SYSTEMDRIVE% - The drive letter of %IIS_BIN%[m
[32m+[m
[32m+[m[32m-->[m
[32m+[m[32m<configuration>[m
[32m+[m
[32m+[m[32m    <!--[m
[32m+[m
[32m+[m[32m        The <configSections> section controls the registration of sections.[m
[32m+[m[32m        Section is the basic unit of deployment, locking, searching and[m
[32m+[m[32m        containment for configuration settings.[m
[32m+[m[41m        [m
[32m+[m[32m        Every section belongs to one section group.[m
[32m+[m[32m        A section group is a container of logically-related sections.[m
[32m+[m[41m        [m
[32m+[m[32m        Sections cannot be nested.[m
[32m+[m[32m        Section groups may be nested.[m
[32m+[m[41m        [m
[32m+[m[32m        <section[m
[32m+[m[32m            name=""  [Required, Collection Key] [XML name of the section][m
[32m+[m[32m            allowDefinition="Everywhere" [MachineOnly|MachineToApplication|AppHostOnly|Everywhere] [Level where it can be set][m
[32m+[m[32m            overrideModeDefault="Allow"  [Allow|Deny] [Default delegation mode][m
[32m+[m[32m            allowLocation="true"  [true|false] [Allowed in location tags][m
[32m+[m[32m        />[m
[32m+[m[41m        [m
[32m+[m[32m        The recommended way to unlock sections is by using a location tag:[m
[32m+[m[32m        <location path="Default Web Site" overrideMode="Allow">[m
[32m+[m[32m            <system.webServer>[m
[32m+[m[32m                <asp />[m
[32m+[m[32m            </system.webServer>[m
[32m+[m[32m        </location>[m
[32m+[m
[32m+[m[32m    -->[m
[32m+[m[32m    <configSections>[m
[32m+[m[32m        <sectionGroup name="system.applicationHost">[m
[32m+[m[32m            <section name="applicationPools" allowDefinition="AppHostOnly" overrideModeDefault="Deny" />[m
[32m+[m[32m            <section name="configHistory" allowDefinition="AppHostOnly" overrideModeDefault="Deny" />[m
[32m+[m[32m            <section name="customMetadata" allowDefinition="AppHostOnly" overrideModeDefault="Deny" />[m
[32m+[m[32m            <section name="listenerAdapters" allowDefinition="AppHostOnly" overrideModeDefault="Deny" />[m
[32m+[m[32m            <section name="log" allowDefinition="AppHostOnly" overrideModeDefault="Deny" />[m
[32m+[m[32m            <section name="serviceAutoStartProviders" allowDefinition="AppHostOnly" overrideModeDefault="Deny" />[m
[32m+[m[32m            <section name="sites" allowDefinition="AppHostOnly" overrideModeDefault="Deny" />[m
[32m+[m[32m            <section name="webLimits" allowDefinition="AppHostOnly" overrideModeDefault="Deny" />[m
[32m+[m[32m        </sectionGroup>[m
[32m+[m
[32m+[m[32m        <sectionGroup name="system.webServer">[m
[32m+[m[32m            <section name="asp" overrideModeDefault="Deny" />[m
[32m+[m[32m            <section name="caching" overrideModeDefault="Allow" />[m
[32m+[m[32m            <section name="cgi" overrideModeDefault="Deny" />[m
[32m+[m[32m            <section name="defaultDocument" overrideModeDefault="Allow" />[m
[32m+[m[32m            <section name="directoryBrowse" overrideModeDefault="Allow" />[m
[32m+[m[32m            <section name="fastCgi" allowDefinition="AppHostOnly" overrideModeDefault="Deny" />[m
[32m+[m[32m            <section name="globalModules" allowDefinition="AppHostOnly" overrideModeDefault="Deny" />[m
[32m+[m[32m            <section name="handlers" overrideModeDefault="Deny" />[m
[32m+[m[32m            <section name="httpCompression" overrideModeDefault="Allow" allowDefinition="Everywhere" />[m
[32m+[m[32m            <section name="httpErrors" overrideModeDefault="Allow" />[m
[32m+[m[32m            <section name="httpLogging" overrideModeDefault="Deny" />[m
[32m+[m[32m            <section name="httpProtocol" overrideModeDefault="Allow" />[m
[32m+[m[32m            <section name="httpRedirect" overrideModeDefault="Allow" />[m
[32m+[m[32m            <section name="httpTracing" overrideModeDefault="Deny" />[m
[32m+[m[32m            <section name="isapiFilters" allowDefinition="MachineToApplication" overrideModeDefault="Deny" />[m
[32m+[m[32m            <section name="modules" allowDefinition="MachineToApplication" overrideModeDefault="Deny" />[m
[32m+[m[32m            <section name="applicationInitialization" allowDefinition="MachineToApplication" overrideModeDefault="Allow" />[m
[32m+[m[32m            <section name="odbcLogging" overrideModeDefault="Deny" />[m
[32m+[m[32m            <sectionGroup name="security">[m
[32m+[m[32m                <section name="access" overrideModeDefault="Deny" />[m
[32m+[m[32m                <section name="applicationDependencies" overrideModeDefault="Deny" />[m
[32m+[m[32m                <sectionGroup name="authentication">[m
[32m+[m[32m                    <section name="anonymousAuthentication" overrideModeDefault="Deny" />[m
[32m+[m[32m                    <section name="basicAuthentication" overrideModeDefault="Deny" />[m
[32m+[m[32m                    <section name="clientCertificateMappingAuthentication" overrideModeDefault="Deny" />[m
[32m+[m[32m                    <section name="digestAuthentication" overrideModeDefault="Deny" />[m
[32m+[m[32m                    <section name="iisClientCertificateMappingAuthentication" overrideModeDefault="Deny" />[m
[32m+[m[32m                    <section name="windowsAuthentication" overrideModeDefault="Deny" />[m
[32m+[m[32m                </sectionGroup>[m
[32m+[m[32m                <section name="authorization" overrideModeDefault="Allow" />[m
[32m+[m[32m                <section name="ipSecurity" overrideModeDefault="Deny" />[m
[32m+[m[32m                <section name="dynamicIpSecurity" overrideModeDefault="Deny" />[m
[32m+[m[32m                <section name="isapiCgiRestriction" allowDefinition="AppHostOnly" overrideModeDefault="Deny" />[m
[32m+[m[32m                <section name="requestFiltering" overrideModeDefault="Allow" />[m
[32m+[m[32m            </sectionGroup>[m
[32m+[m[32m            <section name="serverRuntime" overrideModeDefault="Deny" />[m
[32m+[m[32m            <section name="serverSideInclude" overrideModeDefault="Deny" />[m
[32m+[m[32m            <section name="staticContent" overrideModeDefault="Allow" />[m
[32m+[m[32m            <sectionGroup name="tracing">[m
[32m+[m[32m                <section name="traceFailedRequests" overrideModeDefault="Allow" />[m
[32m+[m[32m                <section name="traceProviderDefinitions" overrideModeDefault="Deny" />[m
[32m+[m[32m            </sectionGroup>[m
[32m+[m[32m            <section name="urlCompression" overrideModeDefault="Allow" />[m
[32m+[m[32m            <section name="validation" overrideModeDefault="Allow" />[m
[32m+[m[32m            <sectionGroup name="webdav">[m
[32m+[m[32m                <section name="globalSettings" overrideModeDefault="Deny" />[m
[32m+[m[32m                <section name="authoring" overrideModeDefault="Deny" />[m
[32m+[m[32m                <section name="authoringRules" overrideModeDefault="Deny" />[m
[32m+[m[32m            </sectionGroup>[m
[32m+[m[32m            <sectionGroup name="rewrite">[m
[32m+[m[32m                <section name="allowedServerVariables" overrideModeDefault="Deny" />[m
[32m+[m[32m                <section name="rules" overrideModeDefault="Allow" />[m
[32m+[m[32m                <section name="outboundRules" overrideModeDefault="Allow" />[m
[32m+[m[32m                <section name="globalRules" overrideModeDefault="Deny" allowDefinition="AppHostOnly" />[m
[32m+[m[32m                <section name="providers" overrideModeDefault="Allow" />[m
[32m+[m[32m                <section name="rewriteMaps" overrideModeDefault="Allow" />[m
[32m+[m[32m            </sectionGroup>[m
[32m+[m[32m            <section name="webSocket" overrideModeDefault="Deny" />[m
[32m+[m[32m        <section name="aspNetCore" overrideModeDefault="Allow" /></sectionGroup>[m
[32m+[m[32m    </configSections>[m
[32m+[m
[32m+[m[32m    <configProtectedData>[m
[32m+[m[32m        <providers>[m
[32m+[m[32m            <add name="IISWASOnlyRsaProvider" type="" description="Uses RsaCryptoServiceProvider to encrypt and decrypt" keyContainerName="iisWasKey" cspProviderName="" useMachineContainer="true" useOAEP="false" />[m
[32m+[m[32m            <add name="AesProvider" type="Microsoft.ApplicationHost.AesProtectedConfigurationProvider" description="Uses an AES session key to encrypt and decrypt" keyContainerName="iisConfigurationKey" cspProviderName="" useOAEP="false" useMachineContainer="true" sessionKey="AQIAAA5mAAAApAAA/HKxkz6alrlAPez0IUgujj/6k3WxCDriHp6jvpv3yEZmo7h6SMzGLxo4mTrIQVHSkB7tmElHKfUFTzE2BWF7nFWHY6Z6qmGBauFzwJMwESjril7Gjz69RBFH259HQ6aRDq9Xfx7U7H4HtdmnKNqGjgl/hwPQBGeIlWiDh+sYv3vKB0QU971tjX6H2B+9armlnC8UOuA6JYMDMI/VLLL16sng0fWAy5JYe0YVABVjiAWDW264RZW9Tr1Oax4qHZKg+SdjULxeOc2YmpX+d0yeITo1HkPF1hN1gHpIPIUDo05ilHUNfR3OkjVCIQK4cFKCq1s8NH+y+13MxUC4Fn1AlQ==" />[m
[32m+[m[32m            <add name="IISWASOnlyAesProvider" type="Microsoft.ApplicationHost.AesProtectedConfigurationProvider" description="Uses an AES session key to encrypt and decrypt" keyContainerName="iisWasKey" cspProviderName="" useOAEP="false" useMachineContainer="true" sessionKey="AQIAAA5mAAAApAAALmU8lTC+v2qtfQiiiquvvLpUQqKLEXs+jSKoWCM/uPhyB++k4dwug19mGidNK5FYiWK2KYE1yhjVJcbp12E98Q0R2nT7eBiCMY2JairxQ591rqABK7keGaIjwH7PwGzSpILl3RJ4YFvJ/7ZXEJxeDZIjW8ZxWVXx+/VyHs9U3WguLEkgMUX3jrxJi8LouxaIVPJAv/YQ1ZCWs8zImitxX/C/7o7yaIxznfsN5nGQzQfpUDPeby99aw2zPVTtZI2LaWIBON8guABvZ6JtJVDWmfdK6sodbnwdZkr6/Z2rfvamT1dC1SpQrGG7ulR/f9/GXvCaW10ZVKxekBF/CYlNMg==" />[m
[32m+[m[32m        </providers>[m
[32m+[m[32m    </configProtectedData>[m
[32m+[m
[32m+[m[32m    <system.applicationHost>[m
[32m+[m
[32m+[m[32m        <applicationPools>[m
[32m+[m[32m            <add name="Clr4IntegratedAppPool" managedRuntimeVersion="v4.0" managedPipelineMode="Integrated" CLRConfigFile="%IIS_USER_HOME%\config\aspnet.config" autoStart="true" />[m
[32m+[m[32m            <add name="Clr4ClassicAppPool" managedRuntimeVersion="v4.0" managedPipelineMode="Classic" CLRConfigFile="%IIS_USER_HOME%\config\aspnet.config" autoStart="true" />[m
[32m+[m[32m            <add name="Clr2IntegratedAppPool" managedRuntimeVersion="v2.0" managedPipelineMode="Integrated" CLRConfigFile="%IIS_USER_HOME%\config\aspnet.config" autoStart="true" />[m
[32m+[m[32m            <add name="Clr2ClassicAppPool" managedRuntimeVersion="v2.0" managedPipelineMode="Classic" CLRConfigFile="%IIS_USER_HOME%\config\aspnet.config" autoStart="true" />[m
[32m+[m[32m            <add name="UnmanagedClassicAppPool" managedRuntimeVersion="" managedPipelineMode="Classic" autoStart="true" />[m
[32m+[m[32m            <applicationPoolDefaults managedRuntimeVersion="v4.0">[m
[32m+[m[32m                <processModel loadUserProfile="true" setProfileEnvironment="false" />[m
[32m+[m[32m            </applicationPoolDefaults>[m
[32m+[m[32m        </applicationPools>[m
[32m+[m
[32m+[m[32m        <!--[m
[32m+[m
[32m+[m[32m          The <listenerAdapters> section defines the protocols with which the[m
[32m+[m[32m          Windows Process Activation Service (WAS) binds.[m
[32m+[m
[32m+[m[32m        -->[m
[32m+[m[32m        <listenerAdapters>[m
[32m+[m[32m            <add name="http" />[m
[32m+[m[32m        </listenerAdapters>[m
[32m+[m
[32m+[m[32m        <sites>[m
[32m+[m[32m            <site name="WebSite1" id="1" serverAutoStart="true">[m
[32m+[m[32m                <application path="/">[m
[32m+[m[32m                    <virtualDirectory path="/" physicalPath="%IIS_SITES_HOME%\WebSite1" />[m
[32m+[m[32m                </application>[m
[32m+[m[32m                <bindings>[m
[32m+[m[32m                    <binding protocol="http" bindingInformation=":8080:localhost" />[m
[32m+[m[32m                </bindings>[m
[32m+[m[32m            </site>[m
[32m+[m[32m            <siteDefaults>[m
[32m+[m[32m                <!-- To enable logging, please change the below attribute "enabled" to "true" -->[m
[32m+[m[32m                <logFile logFormat="W3C" directory="%AppData%\Microsoft\IISExpressLogs" enabled="false" />[m
[32m+[m[32m                <traceFailedRequestsLogging directory="%AppData%\Microsoft" enabled="false" maxLogFileSizeKB="1024" />[m
[32m+[m[32m            </siteDefaults>[m
[32m+[m[32m            <applicationDefaults applicationPool="Clr4IntegratedAppPool" />[m
[32m+[m[32m            <virtualDirectoryDefaults allowSubDirConfig="true" />[m
[32m+[m[32m        </sites>[m
[32m+[m
[32m+[m[32m        <webLimits />[m
[32m+[m
[32m+[m[32m    </system.applicationHost>[m
[32m+[m
[32m+[m[32m    <system.webServer>[m
[32m+[m
[32m+[m[32m        <serverRuntime />[m
[32m+[m
[32m+[m[32m        <asp scriptErrorSentToBrowser="true">[m
[32m+[m[32m            <cache diskTemplateCacheDirectory="%TEMP%\iisexpress\ASP Compiled Templates" />[m
[32m+[m[32m            <limits />[m
[32m+[m[32m        </asp>[m
[32m+[m
[32m+[m[32m        <caching enabled="true" enableKernelCache="true">[m
[32m+[m[32m        </caching>[m
[32m+[m
[32m+[m[32m        <cgi />[m
[32m+[m
[32m+[m[32m        <defaultDocument enabled="true">[m
[32m+[m[32m            <files>[m
[32m+[m[32m                <add value="Default.htm" />[m
[32m+[m[32m                <add value="Default.asp" />[m
[32m+[m[32m                <add value="index.htm" />[m
[32m+[m[32m                <add value="index.html" />[m
[32m+[m[32m                <add value="iisstart.htm" />[m
[32m+[m[32m                <add value="default.aspx" />[m
[32m+[m[32m            </files>[m
[32m+[m[32m        </defaultDocument>[m
[32m+[m
[32m+[m[32m        <directoryBrowse enabled="false" />[m
[32m+[m
[32m+[m[32m        <fastCgi />[m
[32m+[m
[32m+[m[32m        <!--[m
[32m+[m
[32m+[m[32m          The <globalModules> section defines all native-code modules.[m
[32m+[m[32m          To enable a module, specify it in the <modules> section.[m
[32m+[m
[32m+[m[32m        -->[m
[32m+[m[32m        <globalModules>[m
[32m+[m[32m            <add name="HttpLoggingModule" image="%IIS_BIN%\loghttp.dll" />[m
[32m+[m[32m            <add name="UriCacheModule" image="%IIS_BIN%\cachuri.dll" />[m
[32m+[m[32m            <add name="TokenCacheModule" image="%IIS_BIN%\cachtokn.dll" />[m
[32m+[m[32m            <add name="DynamicCompressionModule" image="%IIS_BIN%\compdyn.dll" />[m
[32m+[m[32m            <add name="StaticCompressionModule" image="%IIS_BIN%\compstat.dll" />[m
[32m+[m[32m            <add name="DefaultDocumentModule" image="%IIS_BIN%\defdoc.dll" />[m
[32m+[m[32m            <add name="DirectoryListingModule" image="%IIS_BIN%\dirlist.dll" />[m
[32m+[m[32m            <add name="ProtocolSupportModule" image="%IIS_BIN%\protsup.dll" />[m
[32m+[m[32m            <add name="HttpRedirectionModule" image="%IIS_BIN%\redirect.dll" />[m
[32m+[m[32m            <add name="ServerSideIncludeModule" image="%IIS_BIN%\iis_ssi.dll" />[m
[32m+[m[32m            <add name="StaticFileModule" image="%IIS_BIN%\static.dll" />[m
[32m+[m[32m            <add name="AnonymousAuthenticationModule" image="%IIS_BIN%\authanon.dll" />[m
[32m+[m[32m            <add name="CertificateMappingAuthenticationModule" image="%IIS_BIN%\authcert.dll" />[m
[32m+[m[32m            <add name="UrlAuthorizationModule" image="%IIS_BIN%\urlauthz.dll" />[m
[32m+[m[32m            <add name="BasicAuthenticationModule" image="%IIS_BIN%\authbas.dll" />[m
[32m+[m[32m            <add name="WindowsAuthenticationModule" image="%IIS_BIN%\authsspi.dll" />[m
[32m+[m[32m            <add name="IISCertificateMappingAuthenticationModule" image="%IIS_BIN%\authmap.dll" />[m
[32m+[m[32m            <add name="IpRestrictionModule" image="%IIS_BIN%\iprestr.dll" />[m
[32m+[m[32m            <add name="DynamicIpRestrictionModule" image="%IIS_BIN%\diprestr.dll" />[m
[32m+[m[32m            <add name="RequestFilteringModule" image="%IIS_BIN%\modrqflt.dll" />[m
[32m+[m[32m            <add name="CustomLoggingModule" image="%IIS_BIN%\logcust.dll" />[m
[32m+[m[32m            <add name="CustomErrorModule" image="%IIS_BIN%\custerr.dll" />[m
[32m+[m[32m            <add name="FailedRequestsTracingModule" image="%IIS_BIN%\iisfreb.dll" />[m
[32m+[m[32m            <add name="RequestMonitorModule" image="%IIS_BIN%\iisreqs.dll" />[m
[32m+[m[32m            <add name="IsapiModule" image="%IIS_BIN%\isapi.dll" />[m
[32m+[m[32m            <add name="IsapiFilterModule" image="%IIS_BIN%\filter.dll" />[m
[32m+[m[32m            <add name="CgiModule" image="%IIS_BIN%\cgi.dll" />[m
[32m+[m[32m            <add name="FastCgiModule" image="%IIS_BIN%\iisfcgi.dll" />[m
[32m+[m[32m<!--            <add name="WebDAVModule" image="%IIS_BIN%\webdav.dll" /> -->[m
[32m+[m[32m            <add name="RewriteModule" image="%IIS_BIN%\rewrite.dll" />[m
[32m+[m[32m            <add name="ConfigurationValidationModule" image="%IIS_BIN%\validcfg.dll" />[m
[32m+[m[32m            <add name="WebSocketModule" image="%IIS_BIN%\iiswsock.dll" />[m
[32m+[m[32m            <add name="WebMatrixSupportModule" image="%IIS_BIN%\webmatrixsup.dll" />[m
[32m+[m[32m            <add name="ManagedEngine" image="%windir%\Microsoft.NET\Framework\v2.0.50727\webengine.dll" preCondition="integratedMode,runtimeVersionv2.0,bitness32" />[m
[32m+[m[32m            <add name="ManagedEngine64" image="%windir%\Microsoft.NET\Framework64\v2.0.50727\webengine.dll" preCondition="integratedMode,runtimeVersionv2.0,bitness64" />[m
[32m+[m[32m            <add name="ManagedEngineV4.0_32bit" image="%windir%\Microsoft.NET\Framework\v4.0.30319\webengine4.dll" preCondition="integratedMode,runtimeVersionv4.0,bitness32" />[m
[32m+[m[32m            <add name="ManagedEngineV4.0_64bit" image="%windir%\Microsoft.NET\Framework64\v4.0.30319\webengine4.dll" preCondition="integratedMode,runtimeVersionv4.0,bitness64" />[m
[32m+[m[32m            <add name="ApplicationInitializationModule" image="%IIS_BIN%\warmup.dll" />[m
[32m+[m[32m            <add name="AspNetCoreModule" image="%IIS_BIN%\aspnetcore.dll" />[m
[32m+[m[32m            <add name="AspNetCoreModuleV2" image="%IIS_BIN%\Asp.Net Core Module\V2\aspnetcorev2.dll" />[m
[32m+[m[32m        </globalModules>[m
[32m+[m
[32m+[m[32m        <httpCompression directory="%TEMP%">[m
[32m+[m[32m            <scheme name="gzip" dll="%IIS_BIN%\gzip.dll" />[m
[32m+[m[32m            <dynamicTypes>[m
[32m+[m[32m                <add mimeType="text/*" enabled="true" />[m
[32m+[m[32m                <add mimeType="message/*" enabled="true" />[m
[32m+[m[32m                <add mimeType="application/x-javascript" enabled="true" />[m
[32m+[m[32m                <add mimeType="application/javascript" enabled="true" />[m
[32m+[m[32m                <add mimeType="*/*" enabled="false" />[m
[32m+[m[32m                <add mimeType="text/event-stream" enabled="false" />[m
[32m+[m[32m            </dynamicTypes>[m
[32m+[m[32m            <staticTypes>[m
[32m+[m[32m                <add mimeType="text/*" enabled="true" />[m
[32m+[m[32m                <add mimeType="message/*" enabled="true" />[m
[32m+[m[32m                <add mimeType="application/javascript" enabled="true" />[m
[32m+[m[32m                <add mimeType="application/atom+xml" enabled="true" />[m
[32m+[m[32m                <add mimeType="application/xaml+xml" enabled="true" />[m
[32m+[m[32m                <add mimeType="image/svg+xml" enabled="true" />[m
[32m+[m[32m                <add mimeType="*/*" enabled="false" />[m
[32m+[m[32m            </staticTypes>[m
[32m+[m[32m        </httpCompression>[m
[32m+[m
[32m+[m[32m        <httpErrors lockAttributes="allowAbsolutePathsWhenDelegated,defaultPath">[m
[32m+[m[32m            <error statusCode="401" prefixLanguageFilePath="%IIS_BIN%\custerr" path="401.htm" />[m
[32m+[m[32m            <error statusCode="403" prefixLanguageFilePath="%IIS_BIN%\custerr" path="403.htm" />[m
[32m+[m[32m            <error statusCode="404" prefixLanguageFilePath="%IIS_BIN%\custerr" path="404.htm" />[m
[32m+[m[32m            <error statusCode="405" prefixLanguageFilePath="%IIS_BIN%\custerr" path="405.htm" />[m
[32m+[m[32m            <error statusCode="406" prefixLanguageFilePath="%IIS_BIN%\custerr" path="406.htm" />[m
[32m+[m[32m            <error statusCode="412" prefixLanguageFilePath="%IIS_BIN%\custerr" path="412.htm" />[m
[32m+[m[32m            <error statusCode="500" prefixLanguageFilePath="%IIS_BIN%\custerr" path="500.htm" />[m
[32m+[m[32m            <error statusCode="501" prefixLanguageFilePath="%IIS_BIN%\custerr" path="501.htm" />[m
[32m+[m[32m            <error statusCode="502" prefixLanguageFilePath="%IIS_BIN%\custerr" path="502.htm" />[m
[32m+[m[32m        </httpErrors>[m
[32m+[m
[32m+[m[32m        <httpLogging dontLog="false" />[m
[32m+[m
[32m+[m[32m        <httpProtocol>[m
[32m+[m[32m            <customHeaders>[m
[32m+[m[32m                <clear />[m
[32m+[m[32m                <add name="X-Powered-By" value="ASP.NET" />[m
[32m+[m[32m            </customHeaders>[m
[32m+[m[32m            <redirectHeaders>[m
[32m+[m[32m                <clear />[m
[32m+[m[32m            </redirectHeaders>[m
[32m+[m[32m        </httpProtocol>[m
[32m+[m
[32m+[m[32m        <httpRedirect enabled="false" />[m
[32m+[m
[32m+[m[32m        <httpTracing />[m
[32m+[m
[32m+[m[32m        <isapiFilters>[m
[32m+[m[32m            <filter name="ASP.Net_2.0.50727-64" path="%windir%\Microsoft.NET\Framework64\v2.0.50727\aspnet_filter.dll" enableCache="true" preCondition="bitness64,runtimeVersionv2.0" />[m
[32m+[m[32m            <filter name="ASP.Net_2.0.50727.0" path="%windir%\Microsoft.NET\Framew