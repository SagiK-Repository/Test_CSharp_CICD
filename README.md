문서정보 : 2023.05.09. ~ 05.10. 작성, 작성자 [@SAgiKPJH](https://github.com/SAgiKPJH)

# Test_CSharp_CICD
C#으로 구성한 sln을 CI/CD를 통해 자동 빌드 및 테스트 하여 배포하는 테스트를 만든다.  
:x: 기본적인 내용으로만 이루어져 있으며, 처음 CI/CD 구현을 시도해보는 초점에서 구성한 내용입니다.

### 목표
- [x] : 0. CI/CD?
- [x] : 1. 목표 선정
- [x] : 2. 프로젝트 구성
- [x] : 3. YAML 구성
- [x] : 4. Build & Deploy Test

### 제작자
[@SAgiKPJH](https://github.com/SAgiKPJH)

### 참조

- [Learn Git Hub CI/CD](https://github.com/SagiK-Repository/Learn-Git-Hub-CICD)

<br>

---

<br>

# 0. CI/CD?

- CI/CD는 소프트웨어의 개발, 테스트와 배포를 모두 통합함으로써 소프트웨어 버그를 쉽게 찾아낼 수 있으며, 더 빠른 배포 주기를 가질 수 있게 만들어 준다.
  - CI는 빌드/테스트 자동화 과정
  - CD는 배포 자동화 과정
- CI/CD 기대 효과
  - 반복적 작업 (빌드, 테스트 및 배포)를 자동 처리 할 수 있다.
  - 문제가 있을 때, 경고를 받을 수 있다.
  - 빠르게 사용자에게 배포파일을 제공할 수 있다.
- 참조
  - https://ko.wikipedia.org/wiki/CI/CD
  - https://www.jetbrains.com/ko-kr/teamcity/ci-cd-guide/benefits-of-ci-cd/

<br>

# 1. 목표 선정  

- 다음 조건을 만족한다.  

1. Visual Studio를 통해 C# sln 파일을 만든다. 
2. C# Test 프로젝트를 구성한다.
3. 특정 브랜치에 Commit 될 때 CICD가 동작한다.
4. CICD를 통해 자동 빌드 및 Test를 진행한다.
5. 문제가 없으면 자동 배포된다.

### 다음과 같은 흐름을 구성합니다.

1. 기본적인 빌드 Test 후 Release
   - Main 브랜치가 존재합니다.
   - Main 브랜치에 "Release v0.0.0"으로 특정 규칙에 맞도록 Commit을 합니다.
   - Main Repository 안에 존재하는 sln파일을 빌드합니다.
   - Unit Test Project도 Test 합니다.
   - 결과 문제 없으면 기입한 Tag에 맞춰서 Release를 진행합니다.
2. 실제 상황을 토대로 구성한 Release
   - Main 브랜치와 Develop 브랜치가 존재합니다.
     - Main 브랜치는 Release 전용이고, Develop 브랜치는 개발 전용입니다.
   - Develop에서 개발 후 "Relese v0.0.0"으로 특정 규칙에 맞도록 Commit 합니다.
   - Develop Repository 안에 존재하는 sln파일을 빌드합니다.
   - Unit Test Project도 Test 합니다.
   - 결과 문제 없으면 Main 브랜치에 정해진 특정 파일(dll, exe 파일 따위)를 배포합니다.

* 다양하게 CICD를 구성해볼 수 있는데, 여기서는 1번 방법을 활용해봅니다.


<br><br>

# 2. 프로젝트 구성

- 다음을 만족하는 프로젝트를 구성한다.

1. C# 코드로 작성된 프로젝트
2. .NET Framework Version : 4.7
3. C# 코드에 대한 Unit Test 프로젝트
4. xUnit, FluentAssertions 를 활용
5. 간단한 프로젝트이어야 한다.

- 자세한 내용은 Repository에서 확인할 수 있다.  
- 사전에 Build 및 Test를 진행해 보았다.  
  <img src="https://user-images.githubusercontent.com/66783849/237279443-cd14eab5-9f9a-4922-8a35-ba43538a4d6d.png"/>  
  <img src="https://user-images.githubusercontent.com/66783849/237279545-d55ff204-6135-40c3-8233-db3c2d37e495.png"/>  
  <img src="https://user-images.githubusercontent.com/66783849/237279590-07041f5b-d2d8-41c9-b8aa-9515cef6e1d7.png"/>  

* CI/CD는 별도의 환경에서 진행이 됩니다.
* Window 환경에선 PowerShell을 통해 Build / Test를 CLI로 구성할 수 있습니다.
  ```bash
  # 솔루션 빌드
  docker build project.sln
  
  # 현재 폴더에 존재하면 생략 가능
  docker build
  
  
  # Test
  dotnet test project.csproj
  ```  
  <img src="https://user-images.githubusercontent.com/66783849/237280074-524ad57a-f825-4b22-b072-6fc24502b1f5.png"/>  
  
  
<br><br>

# 3. YAML 구성

- CI/CD는 yml(YAML)을 통해 구성할 수 있다.
- GitHub의 경우 Repositiry의 `Actions`탭에 들어가 구성하거나, .yml 파일을 `.github/workflows`안에 구성합니다.  
  <img src="https://user-images.githubusercontent.com/66783849/247442135-e60a0d61-a4d6-48a4-97a4-b72845208aad.png"/>
- CICD를 다음과 같이 구성한다.
  - window 환경을 사용하기에, 명령어를 Powershell에 맞게 구성한다.
  ```yml
  name: Build and Deploy Test
  
  on:
    push:
      branches:
        - 'main'
  
  jobs:
    build:
      runs-on: windows-latest # 실행할 runner 환경 지정
      
      steps:
      - name: Checkout code
        uses: actions/checkout@v2
        
      - name: Setup .NET
        uses: actions/setup-dotnet@v1
  
      - name: Merge commit message
        run: |
          git log -1 --pretty=%B > message.txt
          echo "::set-output name=message::$(Get-Content message.txt)"
        id: merge_message
  
      - name: Build and Test
        if: startsWith(steps.merge_message.outputs.message, 'Release')
        run: |
          cd CSharpTest
          dotnet build CSharpTest.sln
          dotnet test ./UnitTestProject1/UnitTestProject1.csproj
        # 빌드 및 테스트 실패 시 바로 종료
        # if: ${{ job.status == 'success' }}
  
      # release 태그로부터 version 정보 추출하여 output으로 설정
      - name: Get Release Version
        if: startsWith(steps.merge_message.outputs.message, 'Release') && job.status == 'success'
        run: |
          echo "::set-output name=version::$(("${{steps.merge_message.outputs.message}}" -replace 'Release ', ''))"
        id: extract_release_version
  
      - name: Create release tag
        if: startsWith(steps.merge_message.outputs.message, 'Release') && job.status == 'success'
        uses: actions/create-release@v1 
        env:
          GITHUB_TOKEN: ${{ secrets.GITHUB_TOKEN }}
        with:
          tag_name: ${{ steps.extract_release_version.outputs.version }}
          release_name: Release ${{ steps.extract_release_version.outputs.version }} v${{ github.run_number }}
          body: ${{ steps.merge_message.outputs.message }}
          draft: false
          prerelease: false
           
  ```

<br><br>

# 4. Build & Deploy Test

- 실제 내용을 수정 후, "Release v0.0.2" 내용을 Commit 한다.  
  <img src="https://user-images.githubusercontent.com/66783849/237281247-78bb568f-ed7f-4c6f-b2a7-2afb734a9c70.png"/>  
  <img src="https://user-images.githubusercontent.com/66783849/237281430-e58d9ea8-ab1a-47bb-8ed5-3d183dd8790e.png" width=60%/>  
- 실제로 Build & Test 후 Release를 하는 모습을 확인할 수 있다.  
  <img src="https://user-images.githubusercontent.com/66783849/237281644-7e44e590-fa48-4c5f-9d2c-c18c48892475.png" width=49%/>
  <img src="https://user-images.githubusercontent.com/66783849/237281680-0f88f29f-eca0-4551-9dd2-89fae06c3b05.png" width=49%/>  
- 최종적으로 Release 됨을 확인할 수 있다.  
  <img src="https://user-images.githubusercontent.com/66783849/237281521-6e321831-7a8d-4806-96bc-924eea70c95e.png"/>  
