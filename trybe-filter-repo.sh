### GIT FILTER-REPO ###

## NÃO EXECUTE ESSE SCRIPT DIRETAMENTE
## Esse script foi feito para uso do
## script 'trybe-publisher' fornecido 
## pela Trybe. 

[[ $# == 1 ]] && \
[[ $1 == "trybe-security-parameter" ]] && \
git filter-repo \
    --path .trybe \
    --path .github \
    --path trybe.yml \
    --path trybe-filter-repo.sh \
    --path src/TrybeHotel.Test.Test/ContextTest.cs \
    --path src/TrybeHotel.Test.Test/dockertest.sh \
    --path src/TrybeHotel.Test.Test/dockerlog.txt \
    --path src/TrybeHotel.Test.Test/req01-getStatus.cs \
    --path src/TrybeHotel.Test.Test/req02-dockerfile.cs \
    --path src/TrybeHotel.Test.Test/Usings.cs \
    --path src/TrybeHotel.Test.Test/TrybeHotel.test.test.csproj \
    --path src/TrybeHotel.Test.Test/TrybeHotel.test.test.sln \
    --path src/TrybeHotel.Test.Test \
    --path img/der.png \
    --path docker-compose.yml \
    --path README.md \
    --path .gitignore \
    --invert-paths --force