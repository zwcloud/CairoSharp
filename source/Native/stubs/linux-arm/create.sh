#!/bin/bash

dllName=libcairo.so
soName=libcairo.so.2

echo "generating linux stub for $soName..."

touch empty.c
gcc -shared -o "$soName" empty.c    
gcc -Wl,--no-as-needed -shared -o "$dllName" -fPIC -L. -l:"$soName"
rm -f "$soName"
rm -f empty.c

readelf -d "$dllName"

echo "stub $dllName generated to maps to $soName"
