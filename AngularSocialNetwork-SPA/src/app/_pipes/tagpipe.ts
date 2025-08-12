import { Pipe, PipeTransform } from "@angular/core";

@Pipe({ name: 'hashtag' })

export class HashtagPipe implements PipeTransform {

    private escapeHtml(text: string): string {
    return text
      .replace(/&/g, '&amp;')
      .replace(/</g, '&lt;')
      .replace(/>/g, '&gt;')
      .replace(/"/g, '&quot;')
      /*.replace(/'/g, '&#039;')*/;
  }
  transform(value: string): string {
    if (!value) return '';
    value = this.escapeHtml(value);
    return value.replace(/#(\w+)/g, '<a href="#" class="tag">#$1</a>');
  }
}