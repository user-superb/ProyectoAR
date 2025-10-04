# Aplicaciones móviles 3D, Realidad Virtual y Realidad Aumentada
> Proyectos de Desarrollo de Aplicaciones e Innovación con Alumnos – 2025  
> **Coordinadores:** Esp. Sebastián Dapoto · Lic. Federico Cristina

---

## ✨ Resumen
Aplicación educativa que modela el **diseño y comportamiento de compuertas lógicas** (AND, OR, NOT, XOR) en **AR para celular** y en **MR para Meta Quest 3** con **Unity**.  
Permite **instanciar compuertas**, **conectarlas con cables**, **activar entradas** (con “electrones” o toques), **visualizar estados** y **borrar elementos** con un **tacho de basura** interactivo.

---

## 🎯 Objetivo
Crear una **plataforma interactiva** para ingresantes de la Facultad de Informática que facilite el estudio de **lógica booleana** mediante **tecnologías inmersivas** (AR/MR) con una **interacción intuitiva** (tacto, rayos, hand/controller grab).

---

## 🏗️ Arquitectura y Diseño

**Modelado de compuertas**
- Modelos 3D en **Blender** (cuerpo, entradas, salidas, colliders).
- Diseño visual minimalista con indicadores de estado (rojo/verde).

**AR en celular**
- **AR Foundation** + **ARCore** (Librería de Referencias de Imágenes).
- Cada **QR** ↔ **tipo de compuerta**. Al detectar un QR se instancia el prefab 3D.

**MR en Meta Quest 3**
- **OpenXR** + **Meta XR All-in-One SDK**.
- Building Blocks: **Camera Rig**, **Passthrough**, **Ray Interaction**, **Controller/Hand Tracking**.
- Interacción: **Grab Interaction** para mover/soltar, **Ray/Poke** para UI.

**Lógica booleana y conexiones**
- Interfaces comunes (p.ej. `IEntrada`, `ISalida`, `IOutputProvider`, `IDeletable`).
- **LineRenderer** para cables. Conexión por colisión de colliders de entrada/salida.
- Propagación de valores por **getter** y/o eventos.
- Entradas “libres” conmutables (táctil o “electrones” que activan con contacto).

**Gestión y eliminación**
- **TrashZone** (tacho): al colisionar, invoca `IDeletable.Delete()`.
- Sistema de **layers** para separar interacción (raycast/cápsulas/colliders de puerto).

---

## ✅ Features
- Compuertas: **AND, OR, NOT, XOR**.
- **Instanciación** desde panel (VR) o por **QR** (AR).
- **Cables** interactivos entre compuertas (con feedback visual).
- **Entradas activables** (tap/electrones).
- **Indicadores de estado** (rojo/verde) en salidas/entradas.
- **Borrado** por **tacho de basura**.
- **Soporte dual**: **Android AR** y **Meta Quest 3 MR**.

---

## 🧩 Requisitos
- **Unity 2022 LTS** (recomendado 2022.3.x).
- **AR Foundation** + **ARCore XR Plugin** (para celular).
- **OpenXR** + **Meta XR All-in-One SDK** (para Quest 3).
- **Android SDK/NDK** instalados via Unity Hub.
- Dispositivos:
  - **Android 9+** con soporte ARCore.
  - **Meta Quest 3** (MR).

