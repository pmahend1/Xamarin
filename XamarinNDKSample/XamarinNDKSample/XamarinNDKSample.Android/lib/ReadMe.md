This folder is for sniffing of libraries
Create platform specific directories and place specific .so files within them

Android supported ABI architectures  and hence the folder names will be,
- armeabi
- armeabi-v7a
- arm64-v8a
- x86
- x86_64

Alternatively you can add library so files in Android csproj file include them with AndroidNativeLibrary and ABI as below

	<ItemGroup>
		<AndroidNativeLibrary Include="path/to/libFile.so">
			<Abi>armeabi</Abi>
		</AndroidNativeLibrary>
	</ItemGroup>	

I