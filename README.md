# Unity Toon Shader SRP

## 목차

1. [설치 방법](#1-설치-방법)
2. [개요](#2-개요)
3. [사용 방법](#3-사용-방법)
4. [개발 과정](#4-개발-과정)
5. [기타](#5-기타)

## 개요

### 1. 설치 방법

- [Git URL을 통한 설치 - Unity 매뉴얼](https://docs.unity3d.com/kr/2021.3/Manual/upm-ui-giturl.html)
- [Git 종속성 - Unity 매뉴얼](https://docs.unity3d.com/kr/2021.3/Manual/upm-git.html)

```
https://github.com/E4-Unity/com.eu4ng.toonshader.srp.git?path=/package
```

### 2. 패키지 설명

URP 프로젝트에서 `Unity Toon Shader` 를 적용한 머터리얼의 `Outline` 옵션 활성화 시 SRP 배칭이 깨지면서 `SetPass calls` 가 증가하는 현상이 발생합니다. 이를 보완하기 위해 `Unity Toon Shader 0.9.5-preview` 를 기반으로 제작된 커스텀 패키지입니다.

#### Unity Toon Shader

![image](https://github.com/E4-Unity/unity-toonshader-srp/assets/59055049/39718284-9218-4c72-b382-7c4b6bf2d27b)

#### Unity Toon Shader SRP

![image](https://github.com/E4-Unity/unity-toonshader-srp/assets/59055049/3eea906c-7e1d-4b78-a441-0030510b9ced)

### 3. 사용 방법

`Universal Renderer Data` 에셋의 렌더 피처에 [Unity Toon Shader Outline](#unity-toon-shader-outline) 추가

![image](https://github.com/E4-Unity/unity-toonshader-srp/assets/59055049/b995646c-bf3f-4f17-a2aa-1bdf7288db42)

### 4. 개발 과정

#### 문제 상황

URP 프로젝트에서 Unity Toon Shader 를 적용한 머터리얼의 Outline 옵션 활성화 시 SRP 배치가 깨지면서 `SetPass calls` 가 증가합니다.

#### 원인

`UnityToon.shader`, `UnityToonTessellation.shader` 가 멀티 패스 셰이더인데 URP 에서는 멀티 패스 대신 싱글 패스로 처리하기 때문입니다.

```csharp
// Frame Debugge > Batch cause

SRP: Node use multi-pass shader
```

#### 해결 방법

`Outline Pass` 의 `LightMode` 태그를 커스텀 태그로 변경하여 기본적으로 렌더링되지 않도록 설정한 다음, `RendererFeatures` 에 `LightMode Tags` 에 커스텀 태그가 추가된 `RenderObjects (Experimental)` 를 추가하면 됩니다.

그외에도 `RenderObjects (Experimental) > Filters > Layer Mask` 추가 설정이 필요한데 기본 설정은 `Nothing` 이므로 이를 `Everything` 으로 변경하거나 프로젝트에 맞게 설정해주면 됩니다.

![image](https://github.com/E4-Unity/unity-toonshader-srp/assets/59055049/f7dbfa1e-7ae9-45b4-b7fe-f3625d0b7b25)

### 5. 기타

#### Unity Toon Shader Outline

[해결 방법](#해결-방법)에서 설명한 방식대로 직접 설정하는 것은 번거롭기 때문에 프리셋처럼 사용할 수 있는 렌더 피처입니다.