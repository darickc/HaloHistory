@{
    # Script module or binary module file associated with this manifest
    ModuleToProcess = 'EntityFramework.psm1'

    # Version number of this module.
    ModuleVersion = '7.0'

    # ID used to uniquely identify this module
    GUID = '06e6973e-66a3-4cfc-9a42-3f7d159e945a'

    # Author of this module
    Author = 'Entity Framework Team'

    # Company or vendor of this module
    CompanyName = 'Microsoft Corporation'

    # Copyright statement for this module
    Copyright = '(c) .NET Foundation. All rights reserved.'

    # Description of the functionality provided by this module
    Description = 'Entity Framework PowerShell module used for the Package Manager Console'

    # Minimum version of the Windows PowerShell engine required by this module
    PowerShellVersion = '3.0'

    # Name of the Windows PowerShell host required by this module
    PowerShellHostName = 'Package Manager Host'

    # Minimum version of the Windows PowerShell host required by this module
    PowerShellHostVersion = '1.2'

    # Minimum version of the .NET Framework required by this module
    DotNetFrameworkVersion = '4.0'

    # Minimum version of the common language runtime (CLR) required by this module
    CLRVersion = ''

    # Processor architecture (None, X86, Amd64, IA64) required by this module
    ProcessorArchitecture = ''

    # Modules that must be imported into the global environment prior to importing this module
    RequiredModules = 'NuGet'

    # Assemblies that must be loaded prior to importing this module
    RequiredAssemblies = @()

    # Script files (.ps1) that are run in the caller's environment prior to importing this module
    ScriptsToProcess = @()

    # Type files (.ps1xml) to be loaded when importing this module
    TypesToProcess = @()

    # Format files (.ps1xml) to be loaded when importing this module
    FormatsToProcess = @()

    # Modules to import as nested modules of the module specified in ModuleToProcess
    NestedModules = @()

    # Functions to export from this module
    FunctionsToExport = (
        'Use-DbContext',
        'Add-Migration',
        'Apply-Migration',
        'Update-Database',
        'Script-Migration',
        'Remove-Migration',
        'Enable-Migrations',
        'Scaffold-DbContext'
    )

    # Cmdlets to export from this module
    CmdletsToExport = @()

    # Variables to export from this module
    VariablesToExport = @()

    # Aliases to export from this module
    AliasesToExport = @()

    # List of all modules packaged with this module
    ModuleList = @()

    # List of all files packaged with this module
    FileList = @()

    # Private data to pass to the module specified in ModuleToProcess
    PrivateData = ''
}

# SIG # Begin signature block
# MIIkBwYJKoZIhvcNAQcCoIIj+DCCI/QCAQExDzANBglghkgBZQMEAgEFADB5Bgor
# BgEEAYI3AgEEoGswaTA0BgorBgEEAYI3AgEeMCYCAwEAAAQQH8w7YFlLCE63JNLG
# KX7zUQIBAAIBAAIBAAIBAAIBADAxMA0GCWCGSAFlAwQCAQUABCBlSgsuFzWAZc2s
# V6gS+iVXwe91BZdWePVVAs9o01roSqCCDZIwggYQMIID+KADAgECAhMzAAAAZEeE
# lIbbQRk4AAAAAABkMA0GCSqGSIb3DQEBCwUAMH4xCzAJBgNVBAYTAlVTMRMwEQYD
# VQQIEwpXYXNoaW5ndG9uMRAwDgYDVQQHEwdSZWRtb25kMR4wHAYDVQQKExVNaWNy
# b3NvZnQgQ29ycG9yYXRpb24xKDAmBgNVBAMTH01pY3Jvc29mdCBDb2RlIFNpZ25p
# bmcgUENBIDIwMTEwHhcNMTUxMDI4MjAzMTQ2WhcNMTcwMTI4MjAzMTQ2WjCBgzEL
# MAkGA1UEBhMCVVMxEzARBgNVBAgTCldhc2hpbmd0b24xEDAOBgNVBAcTB1JlZG1v
# bmQxHjAcBgNVBAoTFU1pY3Jvc29mdCBDb3Jwb3JhdGlvbjENMAsGA1UECxMETU9Q
# UjEeMBwGA1UEAxMVTWljcm9zb2Z0IENvcnBvcmF0aW9uMIIBIjANBgkqhkiG9w0B
# AQEFAAOCAQ8AMIIBCgKCAQEAky7a2OY+mNkbD2RfTahYTRQ793qE/DwRMTrvicJK
# LUGlSF3dEp7vq2YoNNV9KlV7TE2K8sDxstNSFYu2swi4i1AL3X/7agmg3GcExPHf
# vHUYIEC+eCyZVt3u9S7dPkL5Wh8wrgEUirCCtVGg4m1l/vcYCo0wbU06p8XzNi3u
# XyygkgCxHEziy/f/JCV/14/A3ZduzrIXtsccRKckyn6B5uYxuRbZXT7RaO6+zUjQ
# hiyu3A4hwcCKw+4bk1kT9sY7gHIYiFP7q78wPqB3vVKIv3rY6LCTraEbjNR+phBQ
# EL7hyBxk+ocu+8RHZhbAhHs2r1+6hURsAg8t4LAOG6I+JQIDAQABo4IBfzCCAXsw
# HwYDVR0lBBgwFgYIKwYBBQUHAwMGCisGAQQBgjdMCAEwHQYDVR0OBBYEFFhWcQTw
# vbsz9YNozOeARvdXr9IiMFEGA1UdEQRKMEikRjBEMQ0wCwYDVQQLEwRNT1BSMTMw
# MQYDVQQFEyozMTY0Mis0OWU4YzNmMy0yMzU5LTQ3ZjYtYTNiZS02YzhjNDc1MWM0
# YjYwHwYDVR0jBBgwFoAUSG5k5VAF04KqFzc3IrVtqMp1ApUwVAYDVR0fBE0wSzBJ
# oEegRYZDaHR0cDovL3d3dy5taWNyb3NvZnQuY29tL3BraW9wcy9jcmwvTWljQ29k
# U2lnUENBMjAxMV8yMDExLTA3LTA4LmNybDBhBggrBgEFBQcBAQRVMFMwUQYIKwYB
# BQUHMAKGRWh0dHA6Ly93d3cubWljcm9zb2Z0LmNvbS9wa2lvcHMvY2VydHMvTWlj
# Q29kU2lnUENBMjAxMV8yMDExLTA3LTA4LmNydDAMBgNVHRMBAf8EAjAAMA0GCSqG
# SIb3DQEBCwUAA4ICAQCI4gxkQx3dXK6MO4UktZ1A1r1mrFtXNdn06DrARZkQTdu0
# kOTLdlGBCfCzk0309RLkvUgnFKpvLddrg9TGp3n80yUbRsp2AogyrlBU+gP5ggHF
# i7NjGEpj5bH+FDsMw9PygLg8JelgsvBVudw1SgUt625nY7w1vrwk+cDd58TvAyJQ
# FAW1zJ+0ySgB9lu2vwg0NKetOyL7dxe3KoRLaztUcqXoYW5CkI+Mv3m8HOeqlhyf
# FTYxPB5YXyQJPKQJYh8zC9b90JXLT7raM7mQ94ygDuFmlaiZ+QSUR3XVupdEngrm
# ZgUB5jX13M+Pl2Vv7PPFU3xlo3Uhj1wtupNC81epoxGhJ0tRuLdEajD/dCZ0xIni
# esRXCKSC4HCL3BMnSwVXtIoj/QFymFYwD5+sAZuvRSgkKyD1rDA7MPcEI2i/Bh5O
# MAo9App4sR0Gp049oSkXNhvRi/au7QG6NJBTSBbNBGJG8Qp+5QThKoQUk8mj0ugr
# 4yWRsA9JTbmqVw7u9suB5OKYBMUN4hL/yI+aFVsE/KJInvnxSzXJ1YHka45ADYMK
# AMl+fLdIqm3nx6rIN0RkoDAbvTAAXGehUCsIod049A1T3IJyUJXt3OsTd3WabhIB
# XICYfxMg10naaWcyUePgW3+VwP0XLKu4O1+8ZeGyaDSi33GnzmmyYacX3BTqMDCC
# B3owggVioAMCAQICCmEOkNIAAAAAAAMwDQYJKoZIhvcNAQELBQAwgYgxCzAJBgNV
# BAYTAlVTMRMwEQYDVQQIEwpXYXNoaW5ndG9uMRAwDgYDVQQHEwdSZWRtb25kMR4w
# HAYDVQQKExVNaWNyb3NvZnQgQ29ycG9yYXRpb24xMjAwBgNVBAMTKU1pY3Jvc29m
# dCBSb290IENlcnRpZmljYXRlIEF1dGhvcml0eSAyMDExMB4XDTExMDcwODIwNTkw
# OVoXDTI2MDcwODIxMDkwOVowfjELMAkGA1UEBhMCVVMxEzARBgNVBAgTCldhc2hp
# bmd0b24xEDAOBgNVBAcTB1JlZG1vbmQxHjAcBgNVBAoTFU1pY3Jvc29mdCBDb3Jw
# b3JhdGlvbjEoMCYGA1UEAxMfTWljcm9zb2Z0IENvZGUgU2lnbmluZyBQQ0EgMjAx
# MTCCAiIwDQYJKoZIhvcNAQEBBQADggIPADCCAgoCggIBAKvw+nIQHC6t2G6qghBN
# NLrytlghn0IbKmvpWlCquAY4GgRJun/DDB7dN2vGEtgL8DjCmQawyDnVARQxQtOJ
# DXlkh36UYCRsr55JnOloXtLfm1OyCizDr9mpK656Ca/XllnKYBoF6WZ26DJSJhIv
# 56sIUM+zRLdd2MQuA3WraPPLbfM6XKEW9Ea64DhkrG5kNXimoGMPLdNAk/jj3gcN
# 1Vx5pUkp5w2+oBN3vpQ97/vjK1oQH01WKKJ6cuASOrdJXtjt7UORg9l7snuGG9k+
# sYxd6IlPhBryoS9Z5JA7La4zWMW3Pv4y07MDPbGyr5I4ftKdgCz1TlaRITUlwzlu
# ZH9TupwPrRkjhMv0ugOGjfdf8NBSv4yUh7zAIXQlXxgotswnKDglmDlKNs98sZKu
# HCOnqWbsYR9q4ShJnV+I4iVd0yFLPlLEtVc/JAPw0XpbL9Uj43BdD1FGd7P4AOG8
# rAKCX9vAFbO9G9RVS+c5oQ/pI0m8GLhEfEXkwcNyeuBy5yTfv0aZxe/CHFfbg43s
# TUkwp6uO3+xbn6/83bBm4sGXgXvt1u1L50kppxMopqd9Z4DmimJ4X7IvhNdXnFy/
# dygo8e1twyiPLI9AN0/B4YVEicQJTMXUpUMvdJX3bvh4IFgsE11glZo+TzOE2rCI
# F96eTvSWsLxGoGyY0uDWiIwLAgMBAAGjggHtMIIB6TAQBgkrBgEEAYI3FQEEAwIB
# ADAdBgNVHQ4EFgQUSG5k5VAF04KqFzc3IrVtqMp1ApUwGQYJKwYBBAGCNxQCBAwe
# CgBTAHUAYgBDAEEwCwYDVR0PBAQDAgGGMA8GA1UdEwEB/wQFMAMBAf8wHwYDVR0j
# BBgwFoAUci06AjGQQ7kUBU7h6qfHMdEjiTQwWgYDVR0fBFMwUTBPoE2gS4ZJaHR0
# cDovL2NybC5taWNyb3NvZnQuY29tL3BraS9jcmwvcHJvZHVjdHMvTWljUm9vQ2Vy
# QXV0MjAxMV8yMDExXzAzXzIyLmNybDBeBggrBgEFBQcBAQRSMFAwTgYIKwYBBQUH
# MAKGQmh0dHA6Ly93d3cubWljcm9zb2Z0LmNvbS9wa2kvY2VydHMvTWljUm9vQ2Vy
# QXV0MjAxMV8yMDExXzAzXzIyLmNydDCBnwYDVR0gBIGXMIGUMIGRBgkrBgEEAYI3
# LgMwgYMwPwYIKwYBBQUHAgEWM2h0dHA6Ly93d3cubWljcm9zb2Z0LmNvbS9wa2lv
# cHMvZG9jcy9wcmltYXJ5Y3BzLmh0bTBABggrBgEFBQcCAjA0HjIgHQBMAGUAZwBh
# AGwAXwBwAG8AbABpAGMAeQBfAHMAdABhAHQAZQBtAGUAbgB0AC4gHTANBgkqhkiG
# 9w0BAQsFAAOCAgEAZ/KGpZjgVHkaLtPYdGcimwuWEeFjkplCln3SeQyQwWVfLiw+
# +MNy0W2D/r4/6ArKO79HqaPzadtjvyI1pZddZYSQfYtGUFXYDJJ80hpLHPM8QotS
# 0LD9a+M+By4pm+Y9G6XUtR13lDni6WTJRD14eiPzE32mkHSDjfTLJgJGKsKKELuk
# qQUMm+1o+mgulaAqPyprWEljHwlpblqYluSD9MCP80Yr3vw70L01724lruWvJ+3Q
# 3fMOr5kol5hNDj0L8giJ1h/DMhji8MUtzluetEk5CsYKwsatruWy2dsViFFFWDgy
# cScaf7H0J/jeLDogaZiyWYlobm+nt3TDQAUGpgEqKD6CPxNNZgvAs0314Y9/HG8V
# fUWnduVAKmWjw11SYobDHWM2l4bf2vP48hahmifhzaWX0O5dY0HjWwechz4GdwbR
# BrF1HxS+YWG18NzGGwS+30HHDiju3mUv7Jf2oVyW2ADWoUa9WfOXpQlLSBCZgB/Q
# ACnFsZulP0V3HjXG0qKin3p6IvpIlR+r+0cjgPWe+L9rt0uX4ut1eBrs6jeZeRhL
# /9azI2h15q/6/IvrC4DqaTuv/DDtBEyO3991bWORPdGdVk5Pv4BXIqF4ETIheu9B
# CrE/+6jMpF3BoYibV3FWTkhFwELJm3ZbCoBIa/15n8G9bW1qyVJzEw16UM0xghXL
# MIIVxwIBATCBlTB+MQswCQYDVQQGEwJVUzETMBEGA1UECBMKV2FzaGluZ3RvbjEQ
# MA4GA1UEBxMHUmVkbW9uZDEeMBwGA1UEChMVTWljcm9zb2Z0IENvcnBvcmF0aW9u
# MSgwJgYDVQQDEx9NaWNyb3NvZnQgQ29kZSBTaWduaW5nIFBDQSAyMDExAhMzAAAA
# ZEeElIbbQRk4AAAAAABkMA0GCWCGSAFlAwQCAQUAoIG6MBkGCSqGSIb3DQEJAzEM
# BgorBgEEAYI3AgEEMBwGCisGAQQBgjcCAQsxDjAMBgorBgEEAYI3AgEVMC8GCSqG
# SIb3DQEJBDEiBCCeGNgAFe/1x+wOCg2ejmZ5k0m6GWdvfJqdSkxqktdpTzBOBgor
# BgEEAYI3AgEMMUAwPqAkgCIATQBpAGMAcgBvAHMAbwBmAHQAIABBAFMAUAAuAE4A
# RQBUoRaAFGh0dHA6Ly93d3cuYXNwLm5ldC8gMA0GCSqGSIb3DQEBAQUABIIBAFHj
# KPgDMK/xmFg3bkmr178rjitalFQghYjuv8+ibt7eLtHcSwteUTs9FD61ZEKjpiJV
# L6YJbOFu3PBKDgeas7XqW6d5ngwJl+nK/mzg2K0DlTHuDCNP36uWRYYgRSQcK27X
# ICJ++BfIuc6cuojtiEEaKp+N52m110gyt3b41wjVwd/IlG8kqyKpBLtyynmwj4aH
# JYGHiyezjEucFhEbVJyr4/EY2ksnbU8VHhPC7Dnr+RbCaMSx236t8q/5GRZrxRk8
# 1EHVuSmZruSEHCdbCHMIIfmXszUeIO8bValoEWsy5MrOps563G491SNiSVfF88Eu
# /BoTuaSKA/Y37CYXVfmhghNJMIITRQYKKwYBBAGCNwMDATGCEzUwghMxBgkqhkiG
# 9w0BBwKgghMiMIITHgIBAzEPMA0GCWCGSAFlAwQCAQUAMIIBOgYLKoZIhvcNAQkQ
# AQSgggEpBIIBJTCCASECAQEGCisGAQQBhFkKAwEwMTANBglghkgBZQMEAgEFAAQg
# re8YlmlRQ1wFmjMS9s07dXL1vyHVLZS36erGetGcqSQCBlYqPzw6nxgTMjAxNTEx
# MTcwMDAyMTYuMTQzWjAEgAIB9KCBuaSBtjCBszELMAkGA1UEBhMCVVMxEzARBgNV
# BAgTCldhc2hpbmd0b24xEDAOBgNVBAcTB1JlZG1vbmQxHjAcBgNVBAoTFU1pY3Jv
# c29mdCBDb3Jwb3JhdGlvbjENMAsGA1UECxMETU9QUjEnMCUGA1UECxMebkNpcGhl
# ciBEU0UgRVNOOjE0OEMtQzRCOS0yMDY2MSUwIwYDVQQDExxNaWNyb3NvZnQgVGlt
# ZS1TdGFtcCBTZXJ2aWNloIIOzzCCBnEwggRZoAMCAQICCmEJgSoAAAAAAAIwDQYJ
# KoZIhvcNAQELBQAwgYgxCzAJBgNVBAYTAlVTMRMwEQYDVQQIEwpXYXNoaW5ndG9u
# MRAwDgYDVQQHEwdSZWRtb25kMR4wHAYDVQQKExVNaWNyb3NvZnQgQ29ycG9yYXRp
# b24xMjAwBgNVBAMTKU1pY3Jvc29mdCBSb290IENlcnRpZmljYXRlIEF1dGhvcml0
# eSAyMDEwMB4XDTEwMDcwMTIxMzY1NVoXDTI1MDcwMTIxNDY1NVowfDELMAkGA1UE
# BhMCVVMxEzARBgNVBAgTCldhc2hpbmd0b24xEDAOBgNVBAcTB1JlZG1vbmQxHjAc
# BgNVBAoTFU1pY3Jvc29mdCBDb3Jwb3JhdGlvbjEmMCQGA1UEAxMdTWljcm9zb2Z0
# IFRpbWUtU3RhbXAgUENBIDIwMTAwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEK
# AoIBAQCpHQ28dxGKOiDs/BOX9fp/aZRrdFQQ1aUKAIKF++18aEssX8XD5WHCdrc+
# Zitb8BVTJwQxH0EbGpUdzgkTjnxhMFmxMEQP8WCIhFRDDNdNuDgIs0Ldk6zWczBX
# JoKjRQ3Q6vVHgc2/JGAyWGBG8lhHhjKEHnRhZ5FfgVSxz5NMksHEpl3RYRNuKMYa
# +YaAu99h/EbBJx0kZxJyGiGKr0tkiVBisV39dx898Fd1rL2KQk1AUdEPnAY+Z3/1
# ZsADlkR+79BL/W7lmsqxqPJ6Kgox8NpOBpG2iAg16HgcsOmZzTznL0S6p/TcZL2k
# AcEgCZN4zfy8wMlEXV4WnAEFTyJNAgMBAAGjggHmMIIB4jAQBgkrBgEEAYI3FQEE
# AwIBADAdBgNVHQ4EFgQU1WM6XIoxkPNDe3xGG8UzaFqFbVUwGQYJKwYBBAGCNxQC
# BAweCgBTAHUAYgBDAEEwCwYDVR0PBAQDAgGGMA8GA1UdEwEB/wQFMAMBAf8wHwYD
# VR0jBBgwFoAU1fZWy4/oolxiaNE9lJBb186aGMQwVgYDVR0fBE8wTTBLoEmgR4ZF
# aHR0cDovL2NybC5taWNyb3NvZnQuY29tL3BraS9jcmwvcHJvZHVjdHMvTWljUm9v
# Q2VyQXV0XzIwMTAtMDYtMjMuY3JsMFoGCCsGAQUFBwEBBE4wTDBKBggrBgEFBQcw
# AoY+aHR0cDovL3d3dy5taWNyb3NvZnQuY29tL3BraS9jZXJ0cy9NaWNSb29DZXJB
# dXRfMjAxMC0wNi0yMy5jcnQwgaAGA1UdIAEB/wSBlTCBkjCBjwYJKwYBBAGCNy4D
# MIGBMD0GCCsGAQUFBwIBFjFodHRwOi8vd3d3Lm1pY3Jvc29mdC5jb20vUEtJL2Rv
# Y3MvQ1BTL2RlZmF1bHQuaHRtMEAGCCsGAQUFBwICMDQeMiAdAEwAZQBnAGEAbABf
# AFAAbwBsAGkAYwB5AF8AUwB0AGEAdABlAG0AZQBuAHQALiAdMA0GCSqGSIb3DQEB
# CwUAA4ICAQAH5ohRDeLG4Jg/gXEDPZ2joSFvs+umzPUxvs8F4qn++ldtGTCzwsVm
# yWrf9efweL3HqJ4l4/m87WtUVwgrUYJEEvu5U4zM9GASinbMQEBBm9xcF/9c+V4X
# NZgkVkt070IQyK+/f8Z/8jd9Wj8c8pl5SpFSAK84Dxf1L3mBZdmptWvkx872ynoA
# b0swRCQiPM/tA6WWj1kpvLb9BOFwnzJKJ/1Vry/+tuWOM7tiX5rbV0Dp8c6ZZpCM
# /2pif93FSguRJuI57BlKcWOdeyFtw5yjojz6f32WapB4pm3S4Zz5Hfw42JT0xqUK
# loakvZ4argRCg7i1gJsiOCC1JeVk7Pf0v35jWSUPei45V3aicaoGig+JFrphpxHL
# mtgOR5qAxdDNp9DvfYPw4TtxCd9ddJgiCGHasFAeb73x4QDf5zEHpJM692VHeOj4
# qEir995yfmFrb3epgcunCaw5u+zGy9iCtHLNHfS4hQEegPsbiSpUObJb2sgNVZl6
# h3M7COaYLeqN4DMuEin1wC9UJyH3yKxO2ii4sanblrKnQqLJzxlBTeCG+SqaoxFm
# MNO7dDJL32N79ZmKLxvHIa9Zta7cRDyXUHHXodLFVeNp3lfB0d4wwP3M5k37Db9d
# T+mdHhk4L7zPWAUu7w2gUDXa7wknHNWzfjUeCLraNtvTX4/edIhJEjCCBNowggPC
# oAMCAQICEzMAAABw0P+kO9J01CEAAAAAAHAwDQYJKoZIhvcNAQELBQAwfDELMAkG
# A1UEBhMCVVMxEzARBgNVBAgTCldhc2hpbmd0b24xEDAOBgNVBAcTB1JlZG1vbmQx
# HjAcBgNVBAoTFU1pY3Jvc29mdCBDb3Jwb3JhdGlvbjEmMCQGA1UEAxMdTWljcm9z
# b2Z0IFRpbWUtU3RhbXAgUENBIDIwMTAwHhcNMTUxMDA3MTgxNzM3WhcNMTcwMTA3
# MTgxNzM3WjCBszELMAkGA1UEBhMCVVMxEzARBgNVBAgTCldhc2hpbmd0b24xEDAO
# BgNVBAcTB1JlZG1vbmQxHjAcBgNVBAoTFU1pY3Jvc29mdCBDb3Jwb3JhdGlvbjEN
# MAsGA1UECxMETU9QUjEnMCUGA1UECxMebkNpcGhlciBEU0UgRVNOOjE0OEMtQzRC
# OS0yMDY2MSUwIwYDVQQDExxNaWNyb3NvZnQgVGltZS1TdGFtcCBTZXJ2aWNlMIIB
# IjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAw2m8nB2Q5qHCveJ7SE0NO3V/
# KtQ1danNsvz4mhwpMTftP59QEJM4wU4/J+rC4BXTYoqoSKyOGVTOw336i2YKtcKP
# HG9qyXT1nlNX182lMvBUUC9poe9jE4VWLVZXJjYpJRSTcBW0ZI53Tzr96MEuUD6E
# cPdH6A++ztkAmKlJ0FjI1kOZLc9IQE9bgUB5XdWecMk4gy9p16+Rtiwqz4BsUPT9
# 9LLvGBKDPEjJ8YrefkjG2AzF06OfECRbtADxQvxXIwIIDTpS3cXnj5x47vdSo8xz
# tf7FhqYq7xl+faH/mEX5bWqeCAmaWHicw7+zvBZF6mWJ+jU59NIswNvzoXHmrQID
# AQABo4IBGzCCARcwHQYDVR0OBBYEFJs2x2j9jX4E3XqiFj4DvYenjpDUMB8GA1Ud
# IwQYMBaAFNVjOlyKMZDzQ3t8RhvFM2hahW1VMFYGA1UdHwRPME0wS6BJoEeGRWh0
# dHA6Ly9jcmwubWljcm9zb2Z0LmNvbS9wa2kvY3JsL3Byb2R1Y3RzL01pY1RpbVN0
# YVBDQV8yMDEwLTA3LTAxLmNybDBaBggrBgEFBQcBAQROMEwwSgYIKwYBBQUHMAKG
# Pmh0dHA6Ly93d3cubWljcm9zb2Z0LmNvbS9wa2kvY2VydHMvTWljVGltU3RhUENB
# XzIwMTAtMDctMDEuY3J0MAwGA1UdEwEB/wQCMAAwEwYDVR0lBAwwCgYIKwYBBQUH
# AwgwDQYJKoZIhvcNAQELBQADggEBACsT/3QL9JltS0iTaOHfX1WWSzPR2z8qIeDj
# Sz5t3QOXme+jqCN84SEtPrwRsbtEqEo9KppPaQvaQLbmMtLhmwA4mYiAk3aoL0YG
# Jm6aMmw9TwQPhYWotyWzmHwiNyKSg5Vs+ki0vu26SuHs14cmwjQxs8//O4uWzNcD
# DbIHxHChoDd2BiCLgrpAi4OqAepNtZv/7KbRIDqal/5ydN57gH0+symglk2imeNy
# +yhTVN9OrbD7hujcJiQZvrL6jcC/5vV3WaaxQQ/LsJtJSB6Umws/OXhnun8q5hpk
# 52zn95JQzvmqnrmt/IOBTO5Ht4fSFVT01NA+tqtteebmnwehRc+hggN4MIICYAIB
# ATCB46GBuaSBtjCBszELMAkGA1UEBhMCVVMxEzARBgNVBAgTCldhc2hpbmd0b24x
# EDAOBgNVBAcTB1JlZG1vbmQxHjAcBgNVBAoTFU1pY3Jvc29mdCBDb3Jwb3JhdGlv
# bjENMAsGA1UECxMETU9QUjEnMCUGA1UECxMebkNpcGhlciBEU0UgRVNOOjE0OEMt
# QzRCOS0yMDY2MSUwIwYDVQQDExxNaWNyb3NvZnQgVGltZS1TdGFtcCBTZXJ2aWNl
# oiUKAQEwCQYFKw4DAhoFAAMVABgykV6+XxSyhp9JnQisIqemDPfPoIHCMIG/pIG8
# MIG5MQswCQYDVQQGEwJVUzETMBEGA1UECBMKV2FzaGluZ3RvbjEQMA4GA1UEBxMH
# UmVkbW9uZDEeMBwGA1UEChMVTWljcm9zb2Z0IENvcnBvcmF0aW9uMQ0wCwYDVQQL
# EwRNT1BSMScwJQYDVQQLEx5uQ2lwaGVyIE5UUyBFU046NERFOS0wQzVFLTNFMDkx
# KzApBgNVBAMTIk1pY3Jvc29mdCBUaW1lIFNvdXJjZSBNYXN0ZXIgQ2xvY2swDQYJ
# KoZIhvcNAQEFBQACBQDZ9ImDMCIYDzIwMTUxMTE2MTY1NzM5WhgPMjAxNTExMTcx
# NjU3MzlaMHYwPAYKKwYBBAGEWQoEATEuMCwwCgIFANn0iYMCAQAwCQIBAAIBDQIB
# /zAHAgEAAgIabjAKAgUA2fXbAwIBADA2BgorBgEEAYRZCgQCMSgwJjAMBgorBgEE
# AYRZCgMBoAowCAIBAAIDB6EgoQowCAIBAAIDB6EgMA0GCSqGSIb3DQEBBQUAA4IB
# AQCxDgN9GMgO1LgR6or2BJy2x+4XvTNP0qb/vPT6Mk335CPGrEntY/coRy+RkNOj
# 2RlWh8cj0yOZTVyfNygzev5mwMW8CLRfRc8VU6/jdAh/p+PO98MZVtt49XFp4HnZ
# 6/g329NiE2B+HrHSJvNtSiTEJFjrGo8QcNqEdigWtM0GVUQWXNTYAyly7shGJ9La
# 5YNDTqEhEX9f+ixUacixvd4olVBDIXv8DptkKsKFOBz+6ClNapfcNU6pcaJ5cPXJ
# +VcuidkVePGpcz1JsF2qU5NvBlyM6G7uDxF/P7gWutSnjzJ+EaVM0CgjleBxDyzJ
# iJAQc2NboRzbtnqQNAx28tBFMYIC9TCCAvECAQEwgZMwfDELMAkGA1UEBhMCVVMx
# EzARBgNVBAgTCldhc2hpbmd0b24xEDAOBgNVBAcTB1JlZG1vbmQxHjAcBgNVBAoT
# FU1pY3Jvc29mdCBDb3Jwb3JhdGlvbjEmMCQGA1UEAxMdTWljcm9zb2Z0IFRpbWUt
# U3RhbXAgUENBIDIwMTACEzMAAABw0P+kO9J01CEAAAAAAHAwDQYJYIZIAWUDBAIB
# BQCgggEyMBoGCSqGSIb3DQEJAzENBgsqhkiG9w0BCRABBDAvBgkqhkiG9w0BCQQx
# IgQg/iBsO8jqWil3hbZ/ft00yyCAHexUCj925Fs6GjAJvC0wgeIGCyqGSIb3DQEJ
# EAIMMYHSMIHPMIHMMIGxBBQYMpFevl8UsoafSZ0IrCKnpgz3zzCBmDCBgKR+MHwx
# CzAJBgNVBAYTAlVTMRMwEQYDVQQIEwpXYXNoaW5ndG9uMRAwDgYDVQQHEwdSZWRt
# b25kMR4wHAYDVQQKExVNaWNyb3NvZnQgQ29ycG9yYXRpb24xJjAkBgNVBAMTHU1p
# Y3Jvc29mdCBUaW1lLVN0YW1wIFBDQSAyMDEwAhMzAAAAcND/pDvSdNQhAAAAAABw
# MBYEFKPh61iOYDg2kFAE7tDlL95jbzvDMA0GCSqGSIb3DQEBCwUABIIBADnmw5TA
# 4a4st3zuG/j+T6B44V5ClXgYvWACbn7OTtvQ/kVvPwUa9GlcZrWbtk6zaQ98bpKh
# qNZLIHAat1bcDbuAK3b7W9WhOHifNLNfIBbAgoRwkSdqrOIhgizIQ+GpE2gnH3TW
# pxvutax4TBwZHjQdUmmpHEtbkp2b5OHzY/wFw84Xsh3HJzYBbWa0nc2NWm2835fr
# kBBFXMiJiIWPeF8BOZatMCGPsMA3+t/49XhV4F3aJjUkoqGLEMHT3pp4nvtu8aXN
# szxfZa8tyT8Verq9Gi6/NvkcYtfKBjXBXMkmzAypTNIG7DGJUS5T42PEBSlFD1u/
# XR4zwVhD9YBX+CA=
# SIG # End signature block
