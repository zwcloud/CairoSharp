# Stubs

The idea is to generate a stub shared library that is named like the Windows DLL 
and contains a reference to the real library name for Linux. The DllImport in .NET code 
remains unchanged and uses the Windows DLL name (without .dll suffix). 
The .NET core native loader will then load the stub shared object. This action 
invokes the Linux dynamic loader (ld.so) which then resolves the dependency of the 
stub on the real library and automatically maps all symbols from the real library 
into the stub.

To generate a stub library do the following:

```
touch empty.c
gcc -shared -o libLinuxName.so empty.c    
gcc -Wl,--no-as-needed -shared -o libWindowsName.so -fPIC -L. -l:libLinuxName.so
rm -f libLinuxName.so
rm -f empty.c
```

[Source](https://github.com/dotnet/coreclr/issues/930#issuecomment-328542896)