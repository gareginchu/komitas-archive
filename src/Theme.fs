module Theme

/// Design tokens for The Reading Room.
/// The palette is materials, not swatches: bone paper, ink, candle-gold.
/// See CLAUDE.md §4 for the authoritative palette.

module Colors =
    let bone        = "#F1EAD8"
    let boneShade   = "#E4DAC0"
    let ink         = "#17130D"
    let ink2        = "#4E4638"
    let rule        = "#C7B891"
    let candle      = "#B8892A"
    let linen       = "#F7F1E2"

module Type =
    /// Display serif for headings and song titles.
    let display = "'Cormorant Garamond', 'Noto Serif Armenian', Georgia, serif"
    /// Body serif for prose.
    let body = "'Source Serif 4', 'Cormorant Garamond', Georgia, serif"
    /// Metadata mono.
    let mono = "'JetBrains Mono', ui-monospace, SFMono-Regular, Menlo, monospace"

module Space =
    /// 8 px vertical baseline. All spacing values are multiples of 8.
    let unit = 8
