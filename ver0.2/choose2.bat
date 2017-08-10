setlocal EnableDelayedExpansion
for %%f in (%CD%\*.html) do (
	Readfile2.exe<%%f>temp.txt
)
coarsehelperver3.exe<temp.txt>answer.txt
