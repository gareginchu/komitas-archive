# Komitas — The Reading Room

**Working title.** Repo: `gareginchu/komitas-archive`. Public URL (once deployed): <https://gareginchu.github.io/komitas-archive/>. Portal: <https://gareginchu.github.io/armenian-composer-heritage/>.

A digital archive around **Komitas Vardapet (Soghomon Soghomonyan, 1869–1935)** — priest, ethnographer, composer. The man who wrote the folk voice down.

This file is the design and engineering spec for the site. Everything below is grounded in the source CD and the discipline of the museum micro-site.

---

## 1. Editorial thesis

Komitas is not primarily a "composer" in the concert-hall sense. He is the ear that heard a peasant sing at a well, wrote it down, and rescued it. Then the Armenian Genocide broke him.

The site should feel like a **reading room** in a manuscript library. Slow. Quiet. Attentive. The visitor is invited to look at a sheet of music, listen to a recording, and read what Komitas wrote next to it. Nothing more.

The dominant motif: **the voice preserved**. Every room comes back to it.

---

## 2. Source material

Read-only source: `C:\Users\gareg\OneDrive\Desktop\komitas`.

- `data/pdf/` — **34 sheet-music PDFs.** Songs (Antuni, Krunk, Garun a, Hov areq, Eri eri, Es arun…), the Divine Liturgy and its fragments, biographies of his circle (Manuk Abeghyan, Hrachya Acharyan, Margarit Babayan, Mesrovb Maranjean, Panos Terlemezyan, Garegin Levonyan), plus the reference book *Komitas Vardapet*.
- `data/sound/music/` — **574 audio files.** Curator selects the 30–60 that ship in Phase 1.
- `data/video/clips/` — **102 video clips.** FLV format; transcode to MP4 (H.264 + AAC) on ingest.
- `data/images/bigs/gallery/` — **464 gallery photographs.**
- `data/images/bigs/timeline/` — **137 timeline images.**
- `data/images/bigs/text/` — **43 text images** (probably scanned pages).
- `data/text/{am,en,ru}/` — trilingual XML metadata: `menu.xml`, `timeline.xml`, `photo.xml`, `sound.xml`, `video.xml`, `text.xml`, `config.xml`, `final_screen.xml`. XML text nodes may be Windows-1251 encoded; normalise to UTF-8 on ingest.

**Never fabricate** a date, an attribution, a lyric, or a song title. If it isn't in one of these files (or in a source the curator has cited in `src/content/sources.bib`), it does not go on the site.

---

## 3. Design principles

1. **The manuscript is the interface.** Sheet music and audio are the primary content types. UI recedes.
2. **Silence is content.** Whitespace, blank pages between sections, unaccompanied audio without imagery. The Genocide left Komitas silent for 20 years. The site respects that with negative space.
3. **One accent only.** A single warm gold, used sparingly.
4. **Trilingual by default.** Armenian / English / Russian. Never one dominant.
5. **Slow web.** Static, lightweight, no motion beyond fades. Works on 3G. Degrades to a legible document without JavaScript.
6. **Honesty about scarcity.** Where a source is uncertain, say so on the page. Where audio is a modern performance rather than a field recording, say so.

Anti-goals: parallax hero sections, autoplaying music, "immersive" mode, AI voice cloning, generative decorative art, marbled paper backgrounds, sepia filters over anything.

---

## 4. Design system

### 4.1 Palette

Materials, not swatches. Ceiling values — dial back until they feel weightless.

```
--bone         #F1EAD8   /* the paper. Warm off-white with a hint of yellow. */
--bone-shade   #E4DAC0   /* deeper paper, plate backgrounds */
--ink          #17130D   /* body text. Warm near-black. */
--ink-2        #4E4638   /* secondary text, captions */
--rule         #C7B891   /* hairline dividers */
--candle       #B8892A   /* the one accent. Candle-gold. Links, current-section marker, section numerals. */
--linen        #F7F1E2   /* card / sheet-music background, slightly lighter than bone */
```

No gradients. No shadows. Lift is done with hairlines and 24 px of margin.

### 4.2 Typography

Two families, no more.

- **Display serif for headings, song titles, chapter openers.**
  Primary choice: **Cormorant Garamond** (SIL OFL) — a lyrical, high-contrast Garamond revival with a wide range of weights. Its display cut has the right ceremonial quality for song titles.
  Armenian coupling: **Noto Serif Armenian** at matching optical size. When paired with Cormorant, set Armenian at +1 body size to match the x-height.
- **Body serif for prose.**
  **Source Serif 4** at 18 px, line-height 1.65, measure 62–68 ch. Sober, workhorse, readable at length.
- **Metadata mono.**
  **JetBrains Mono** at 13 px for provenance lines (source · date · holder · licence) and for XML/text snippets when quoted.

Rules:
- Never mix a sans in the body. If a small caps label is needed, use Cormorant Small Caps.
- Armenian passages tagged `lang="hy"` for screen-reader correctness.
- Never italicise Armenian; use weight for emphasis instead.
- Song titles: display serif italics, always in Armenian first (with Latin transliteration in parentheses on first use).
- Numerals: old-style lining figures in body; tabular figures only in tables of contents.

### 4.3 Grid & spacing

- 12-column grid, 88 px column, 24 px gutter. Content region caps at 1200 px.
- Sheet-music plates break to viewport edge at ≥1200 px.
- 8 px vertical baseline. Every spacing value is a multiple of 8.
- Breakpoints: 360 / 720 / 1024 / 1440 / 1920.

### 4.4 Motion

- Global rule: no motion except opacity fades on scroll-in.
- Prefers-reduced-motion: hard off, even the fades.
- The one exception: the manuscript viewer's cursor-linked highlight of the currently-hovered bar. Click-to-toggle on touch and reduced-motion.

### 4.5 Imagery treatment

- Portraits shown at native aspect ratio, 24 px of `--bone` around them. Never CSS filters over historical images. Never duotones.
- Sheet music shown in a deep-zoom IIIF viewer (OpenSeadragon over pre-built tile pyramids).
- Captions below every image: small-caps title in display serif, mono provenance line `Source · Date · Holder · Licence · Confidence: verified | attributed | uncertain`.
- Aspect-ratio boxes always pre-reserved to prevent layout shift.
- Audio waveforms drawn statically at build time (peaks JSON), never live. Warm gold on bone. No animated playhead — a static tick that jumps on the second.

---

## 5. Information architecture

Seven rooms. Reachable from a hush-quiet horizontal rule at the top of every page.

```
/                     Threshold             (landing)
/life                 Life                  (biography)
/songs                Songs                 (transcriptions, per-song reading pages)
/liturgy              Liturgy               (Divine Liturgy fragments)
/circle               The Circle            (biographies of his associates, from PDFs)
/silence              Silence               (1916–1935: the years after the Genocide)
/sources              Sources & Colophon    (bibliography, licences, AI disclosure)
```

Nothing else. No search box in Phase 1 — the site is short enough to read cover-to-cover.

---

## 6. Page specifications

Each page is a *reading page*. Long-form, generous. Two columns only on `/songs` per-item pages.

### 6.1 `/` — Threshold

- Full-bleed plate of a manuscript page (curator-selected from `data/images/bigs/gallery/`).
- One line in display serif: *Կոմիտաս. Ձայնը պահված.* — *Komitas. The voice preserved.*
- Below the plate, a short paragraph in Armenian, then English, then Russian.
- A single line footer: `Enter →` linking to `/life`.
- No cards, no tiles, no CTA buttons, no audio autoplay.

### 6.2 `/life`

- Portrait plate (verified Komitas photograph).
- Long-form biography, chapter-headed. Every date footnoted to `sources.bib`.
- Armenian version present via a language toggle in the top-right of the article; content lives in `content/life.{am,en,ru}.md`.
- Inline audio player: one recording the biography references (e.g., a village transcription he collected).

### 6.3 `/songs`

- Index page: a list, not a grid. One column, generous line-height. Each song shows: title (Armenian + Latin), one-line provenance, optional listen icon.
- Song reading page (`/songs/:slug`):
  - Sheet-music plate in an IIIF viewer, top of page.
  - Below it: transcription of the lyric, Armenian original + English translation + Russian translation, in three columns on desktop, stacked on mobile.
  - An audio player when a recording exists. If it's a modern performance, a mono line under the transport reads: `Performance · Ensemble · Year · not a field recording`.
  - A curator's paragraph on the song: where and when Komitas collected it, what it's about.
  - Footer: source (which PDF), holder, licence.

### 6.4 `/liturgy`

- Longer editorial framing (the significance of the Divine Liturgy in Komitas's output).
- The Liturgy laid out as a sequence of movements, each with sheet-music plate + audio + short description.
- Bilingual liturgical text where present.

### 6.5 `/circle`

- Short editorial framing (why the people around him matter — Abeghyan, Acharyan, Babayan, Maranjean, Terlemezyan, Levonyan).
- Six or so profile blocks, each: a portrait (from CD gallery if identifiable), a paragraph, a citation to the source PDF.
- No page-per-person unless the source material justifies it.

### 6.6 `/silence`

- The years 1916–1935 — Komitas after the arrest of 24 April 1915, in institutions in Constantinople and Paris.
- No music autoplay on this page. Long, restrained, almost entirely text.
- Sparse imagery: one plate at top, one at bottom.
- A single audio option: the *last* recording (or field recording) attributed to him.
- Ends on a single sentence and a rule.

### 6.7 `/sources`

- Full bibliography, numbered footnotes referenced from all pages.
- Provenance table for every asset that ships.
- AI disclosure (see §7).
- Licences for editorial content (CC BY-NC 4.0), sheet music PDFs (their own licence), audio (per-track), fonts, libraries.
- Credits: editor, curator, engineer, designer.
- Contact.

---

## 7. AI, used with restraint

Only ever a *lens*, never a *voice*.

### Permitted (Phase 2, after the base site is stable)

1. **PDF OCR + verse alignment.** OCR the 34 sheet-music PDFs to extract lyric text; align to audio waveforms so the visitor can click a line and hear it. Human-reviewed before published.
2. **Multilingual search.** A tiny local vector index over editorial prose (Armenian, English, Russian). Queries in any of the three return matched sentences. Client-side, no external API at request time.
3. **Alt-text drafting.** Vision-LM drafts alt text for portraits and sheet-music plates; curator reviews and signs off before shipping. Only reviewed alt-text ships.
4. **Metadata normalisation.** Reconcile the Windows-1251 Cyrillic XML into clean UTF-8 with confidence flags on ambiguous glyphs.

### Forbidden

- Generative art or "in-the-style-of-Komitas" imagery.
- AI-composed music.
- Voice cloning of Komitas (there are no confirmed recordings of his voice; even if there were, no).
- Chatbot impersonation.
- AI-restored or AI-colourised historical portraits.
- Any AI feature not disclosed on `/sources`.

Every AI-touched output is logged in `src/data/ai-manifest.yml` in the same commit that ships it.

---

## 8. Technology

### 8.1 Stack (locked)

**Fable 4.24.0** + **Feliz** (React DSL) + **Vite** + **React 18**.

F# for logic and content transforms; Feliz for view components; Vite for bundling; static export via `vite build`.

Fable 5 was requested first but is broken upstream on this machine: its NuGet package is missing `DotnetToolSettings.xml`, which blocks `dotnet tool install` on both .NET 8.0.422 and 9.0.315 SDKs and survives a full NuGet cache clear. Fable 4.24.0 installs cleanly and produces functionally equivalent output for a static content site. Upgrade to Fable 5 when the upstream package is fixed.

Required tooling on this machine:
- .NET 9 SDK 9.0.315 (installed).
- Node 22.19.0, npm 10.9.3 (installed).
- Fable is a local `dotnet` tool, restored via `dotnet tool restore` after clone.

Design tokens in §4 are stack-independent and are the source of truth.

### 8.2 Repository layout (target)

```
/
├── CLAUDE.md                        (this file)
├── package.json
├── vite.config.ts                    (Fable path) or astro.config.mjs (fallback)
├── src/
│   ├── App.fs / App.fsproj           (Fable path) or src/pages/*.astro (fallback)
│   ├── pages/                        (route components)
│   ├── components/
│   │   ├── Plate.*                   (image plate with caption)
│   │   ├── AudioPlayer.*             (native <audio> + static waveform)
│   │   ├── ManuscriptViewer.*        (OpenSeadragon wrapper)
│   │   ├── TrilingualBlock.*         (am/en/ru language switcher)
│   │   └── Caption.*                 (small-caps + mono provenance line)
│   ├── content/
│   │   ├── life.{am,en,ru}.md
│   │   ├── songs/*.yml               (per-song metadata + lyrics)
│   │   ├── liturgy/*.yml
│   │   ├── circle/*.yml
│   │   ├── silence.{am,en,ru}.md
│   │   └── sources.bib
│   ├── data/
│   │   ├── assets.yml                (canonical inventory + provenance)
│   │   ├── ai-manifest.yml           (every AI feature + disclosure line)
│   │   └── media-manifest.yml        (CD path → R2 URL → licence)
│   └── styles/
│       ├── tokens.css                (§4.1)
│       ├── type.css                  (§4.2)
│       └── layout.css                (§4.3)
├── public/
│   └── assets/                       (thumbnails, IIIF tile pyramids for sheet music)
└── .github/workflows/deploy-pages.yml
```

### 8.3 Media pipeline

- Audio: transcode to 128 kbps AAC (m4a) for streaming, 320 kbps MP3 for download. Store both in R2. Generate static waveform peaks JSON at build time.
- Video: FLV → MP4 (H.264 + AAC), 720p max. Store in R2. Only thumbnails + poster frames in the repo.
- Sheet music: PDF → per-page 300 DPI PNG → IIIF tile pyramid via `vips dzsave`. Ship the pyramid statically from Pages. Provide the original PDF as a download link.
- Images: pipe through `sharp` for AVIF/WebP/JPEG, three sizes (thumbnail 480, view 1200, plate 2400). Blurhash placeholders inline.

### 8.4 Hosting

- **Site:** GitHub Pages via Actions (matches current `armenian-composer-heritage` deployment).
- **Media:** Cloudflare R2 (bucket `komitas-archive-media`). Custom domain later; direct R2 URL is fine for Phase 1.
- **Fonts:** self-hosted WOFF2. Never Google Fonts CDN.
- **Analytics:** Plausible self-hosted or none. Never GA.

### 8.5 JavaScript budget

- Landing, life, silence, sources: 0 KB JS.
- Songs, liturgy: ≤ 20 KB gzipped (audio player + small language switcher).
- Manuscript viewer pages: ≤ 130 KB gzipped (OpenSeadragon dominates).

---

## 9. Accessibility

- WCAG 2.2 AA on every page. Axe + pa11y in CI.
- Every Armenian passage tagged `lang="hy"`, every Russian passage `lang="ru"`.
- Full keyboard reach on all interactive elements. Visible focus rings in `--candle`.
- `prefers-reduced-motion`: all fades disabled, the manuscript viewer's cursor highlight becomes click-to-toggle.
- Contrast: body ≥ 8:1 on `--bone`, small text ≥ 7:1. `--candle` only on ≥18 px or bold.
- Every audio file has a written description; where a song has a lyric, a bilingual transcript.
- No auto-advancing carousels. No flashing content.

---

## 10. Provenance and licensing

`src/data/assets.yml` is the source of truth for every asset that ships. Each entry:

```yaml
- id: gallery-0074
  source: CD/data/images/bigs/gallery/74.jpg
  dated: c. 1905
  holder: (from Komitas Museum-Institute / TK)
  licence: (TK — verify)
  confidence: attributed         # verified | attributed | uncertain
  notes: portrait, seminary period
  used_on: [/life, /threshold]
```

- Uncertain assets do not ship.
- Editorial text: CC BY-NC 4.0 unless a curator note overrides per page.
- Sheet-music PDFs: whatever licence the CD claims; verify with the Komitas Museum-Institute before publishing.
- Third-party fonts and libraries: listed on `/sources`.

---

## 11. Editorial standards

- Never invent a date, a lyric, a title, an attribution.
- Every biographical claim cites `sources.bib`, referenced by a footnote number.
- Armenian names appear in Armenian script first, then a Latin transliteration in parentheses on first use.
- Julian / Gregorian dates disambiguated where the source is unambiguous; otherwise state "OS" or "NS".
- Prefer *Կոմիտաս Վարդապետ* over *Soghomon Soghomonyan* in body copy (his chosen name); use *Soghomon Soghomonyan* on `/life` where the biographical fact matters.
- Song titles always Armenian first, transliteration and English translation on first use.

---

## 12. Roadmap

### Phase 1 — the reading room stands up (target: 4–6 weeks)
- Design system (§4) live.
- All 7 routes with content shells and real trilingual biography ingested from CD XML (§2).
- 8–10 songs on `/songs`, each with sheet music (IIIF pyramid) + audio + trilingual lyric.
- The Divine Liturgy on `/liturgy`, one representative movement fully wired.
- `/circle` with the six PDF-sourced profiles.
- `/silence` written and reviewed.
- `/sources` complete for everything shipped.
- Accessibility audit passing.

### Phase 2 — AI as a lens (target: 4 weeks after Phase 1)
- OCR + verse-audio alignment on the ~34 song PDFs.
- Multilingual local search over editorial prose.
- Curator-reviewed alt-text on every plate.
- Metadata normalisation from CD XML → clean UTF-8 YAML.

### Phase 3 — expansion (open-ended)
- Grow `/songs` from 10 to the full 34 sheet-music PDFs.
- Ingest the 100+ videos, transcode, publish with subtitles.
- Reach out to the Komitas Museum-Institute in Yerevan for provenance verification and additional scans.

---

## 13. Governance

- **Editor:** Maggie Goshin. Every published page is her sign-off.
- **Curator (attributions, sources, translation):** TBD, named on `/sources`.
- **Engineer / designer:** working from this file.
- **Change control:**
  - Editorial content: PR + editor approval before merge.
  - Assets: PR + `assets.yml` update + curator note.
  - AI features: PR + `ai-manifest.yml` update + `/sources` copy.

---

## 14. Rules for Claude working in this repo

- Do not add features not in §5, §6, or the roadmap. Ask first.
- Do not modify anything under `C:\Users\gareg\OneDrive\Desktop\komitas` (read-only source CD).
- Do not import content from the sister archive `khachaturian-archive`.
- Never fabricate a date, place, work title, lyric, or attribution.
- If you are about to write biographical text: stop and ask the curator for a source.
- Prefer editing existing files. New files require a reason.
- Every AI-touched output logged in `src/data/ai-manifest.yml` in the same commit.
- The palette in §4.1 is authoritative — do not introduce new hues.
