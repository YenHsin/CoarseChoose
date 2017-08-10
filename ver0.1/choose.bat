setlocal EnableDelayedExpansion
for %%f in (%CD%\*.html) do (
	Readfile.exe<%%f>temp.txt
)
coarsehelperver2.exe<temp.txt>answer.txt
